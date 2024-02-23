using Amazon.Runtime;
using Amazon.S3;

namespace BE.TradeeHub.PriceBookService.Application.Extensions;

public static class AwsExtensions
{
    public static void AddAwsServices(this IServiceCollection services, IConfiguration configuration,
        AppSettings appSettings)
    {
        if (appSettings.Environment.Contains("dev", StringComparison.CurrentCultureIgnoreCase))
        {
            // For local development, use the default AWS credentials profile
            services.AddAWSService<IAmazonS3>();
        }
        else
        {
            // For production/docker environment, use credentials specified through environment variables
            var awsOptions = configuration.GetAWSOptions();
            awsOptions.Credentials = new BasicAWSCredentials(
                appSettings.AwsAccessKeyId,
                appSettings.AwsSecretAccessKey
            );

            services.AddSingleton<IAmazonS3>(
                sp => new AmazonS3Client(awsOptions.Credentials, appSettings.AWSRegion)
            );
        }
    }
}