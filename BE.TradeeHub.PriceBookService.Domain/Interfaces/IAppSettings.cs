namespace BE.TradeeHub.PriceBookService.Domain.Interfaces;

public interface IAppSettings
{
    public string MongoDbConnectionString { get; set; }
    public string MongoDbDatabaseName { get; set; }
    public string AppClientId { get; set; }
}