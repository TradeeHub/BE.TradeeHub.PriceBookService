namespace BE.TradeeHub.PriceBookService.Domain.Interfaces;

public interface IAppSettings
{
    public string MongoDbConnectionString { get; }
    public string MongoDbDatabaseName { get; }
    public string AppClientId { get; }
    public string S3BucketName { get; }
    public string CloudFrontUrl { get; }
}