using BE.TradeeHub.PriceBookService.Application;
using BE.TradeeHub.PriceBookService.Application.Extensions;
using BE.TradeeHub.PriceBookService.Application.Interfaces;
using BE.TradeeHub.PriceBookService.Application.Services;
using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;
using BE.TradeeHub.PriceBookService.Infrastructure;
using BE.TradeeHub.PriceBookService.Infrastructure.Extensions;
using BE.TradeeHub.PriceBookService.Infrastructure.Repositories;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);
var appSettings = new AppSettings(builder.Configuration);

builder.Services.AddSingleton<IAppSettings>(appSettings);
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<UserContext>();
builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();
builder.Services.AddScoped<IPriceBookService, PriceBookService>();
builder.Services.AddScoped<IPriceBookRepository, PriceBookRepository>();
builder.Services.AddAwsServices(builder.Configuration, appSettings);

builder.Services.AddCors(appSettings).AddAuth(appSettings);

builder.Services
    .AddGraphQLServer()
    .InitializeOnStartup()
    .AddGlobalObjectIdentification()
    .AddAuthorization()
    .AddTypes()
    .BindRuntimeType<ObjectId, IdType>()
    .AddTypeConverter<ObjectId, string>(o => o.ToString())
    .AddTypeConverter<string, ObjectId>(o => ObjectId.Parse(o))
    .AddType<UploadType>()
    .AddMongoDbSorting()
    .AddMongoDbProjections()
    .AddMongoDbPagingProviders()
    .AddMongoDbFiltering();

var app = builder.Build();

app.ExportGraphQlSchemaToFile();

var dbContext = app.Services.GetRequiredService<IMongoDbContext>();
dbContext.EnsureIndexesCreated();

app.UseCors("GraphQLCorsPolicy");
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