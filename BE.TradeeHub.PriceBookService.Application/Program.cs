using Amazon.Runtime;
using Amazon.S3;
using BE.TradeeHub.PriceBookService.Application;
using BE.TradeeHub.PriceBookService.Application.GraphQL.Mutations;
using BE.TradeeHub.PriceBookService.Application.GraphQL.Queries;
using BE.TradeeHub.PriceBookService.Application.Interfaces;
using BE.TradeeHub.PriceBookService.Application.Services;
using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;
using BE.TradeeHub.PriceBookService.Infrastructure;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var appSettings = new AppSettings(builder.Configuration);

        builder.Services.AddSingleton<IAppSettings>(appSettings);
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<UserContext>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("GraphQLCorsPolicy", builder =>
            {
                builder.WithOrigins(appSettings.AllowedDomains) // Replace with the client's URL
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        builder.Services.AddSingleton<MongoDbContext>();
        builder.Services.AddAWSService<IAmazonS3>();
        builder.Services.AddScoped<IPriceBookService, PriceBookService>();
// builder.Services.AddScoped<IExternalReferenceRepository, ExternalReferenceRepository>();
// builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
// builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
// builder.Services.AddScoped<ICommentRepository, CommentRepository>();
// builder.Services.AddScoped<ICustomerService, CustomerService>();
// builder.Services.AddScoped<TypeResolver>();
// builder.Services.AddScoped<CustomerPropertiesDataLoader>();
// builder.Services.AddScoped<CustomerCommentsDataLoader>();
        if (appSettings.Environment.Contains("dev", StringComparison.CurrentCultureIgnoreCase))
        {
            // For development environment, rely on the default credential provider chain
            // which will automatically use credentials from your AWS profile configured in the AWS Toolkit for JetBrains Rider
            builder.Services.AddAWSService<IAmazonS3>();
        }
        else
        {
            // For production/docker environment, use credentials specified through environment variables
            // and manually create and register the AmazonS3Client
            var awsOptions = builder.Configuration.GetAWSOptions();
            awsOptions.Credentials = new BasicAWSCredentials(
                appSettings.AwsAccessKeyId, 
                appSettings.AwsSecretAccessKey
            );
            builder.Services.AddSingleton<IAmazonS3>(sp => new AmazonS3Client(awsOptions.Credentials, appSettings.AWSRegion)
            );
        }

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = appSettings.ValidIssuer;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = appSettings.ValidIssuer,
                ValidateLifetime = true,
                ValidAudience = appSettings.AppClientId,
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    if (context.Request.Cookies.ContainsKey("jwt"))
                    {
                        context.Token = context.Request.Cookies["jwt"];
                    }

                    return Task.CompletedTask;
                }
            };
        });

// builder.Services.AddSingleton<IMongoCollection<CustomerEntity>>(serviceProvider =>
// {
//     var mongoDbContext = serviceProvider.GetRequiredService<MongoDbContext>();
//     return mongoDbContext.Customers; // Assuming this is the property name in MongoDbContext for the collection
// });
//
// builder.Services.AddSingleton<IMongoCollection<PropertyEntity>>(serviceProvider =>
// {
//     var mongoDbContext = serviceProvider.GetRequiredService<MongoDbContext>();
//     return mongoDbContext.Properties; // Assuming this is the property name in MongoDbContext for the collection
// });

        builder.Services.AddAuthorization();

        builder.Services
            .AddGraphQLServer()
            .AddGlobalObjectIdentification()
            .AddAuthorization()
            // .AddDataLoader<CustomerPropertiesDataLoader>()
            // .AddDataLoader<CustomerCommentsDataLoader>()
            .BindRuntimeType<ObjectId, IdType>()
            .AddTypeConverter<ObjectId, string>(o => o.ToString())
            .AddTypeConverter<string, ObjectId>(o => ObjectId.Parse(o))
            .AddQueryType<Query>()
            .AddMutationType<Mutation>()
            // .AddType<CustomerType>()
            // .AddType<PropertyType>()
            // .AddType<CommentType>()
            // .AddType<UserType>()
            // .AddType<ReferenceInfoType>()
            // .AddType<ExternalReferenceType>()
            .AddMongoDbSorting()
            .AddMongoDbProjections()
            .AddMongoDbPagingProviders()
            .AddMongoDbFiltering();

        var app = builder.Build();

        if (appSettings.Environment is "Development" or "Docker")
        {
            var resolver = app.Services.GetService<IRequestExecutorResolver>();
            var executor = resolver?.GetRequestExecutorAsync().Result;
            if (executor != null)
            {
                const string schemaFile = "schema.graphql";
                var newSchema = executor.Schema.ToString();
                var oldSchema = File.ReadAllText(schemaFile);
                if (newSchema != oldSchema)
                    File.WriteAllText(schemaFile, newSchema);
            }
        }

        app.UseCors("GraphQLCorsPolicy"); // Apply the CORS policy
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseRouting();
        app.MapGraphQL();

        app.Use(async (context, next) =>
        {
            if (context.Request.Path == "/")
            {
                context.Response.Redirect("/graphql/", permanent: false);
            }
            else
            {
                await next();
            }
        });

        app.Run();
    }
}