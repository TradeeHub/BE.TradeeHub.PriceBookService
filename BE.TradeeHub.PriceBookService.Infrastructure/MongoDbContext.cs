using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Infrastructure;

public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly MongoClient _client; // Store the MongoClient instance

    public MongoDbContext(IAppSettings appSettings)
    {
        var settings = MongoClientSettings.FromConnectionString(appSettings.MongoDbConnectionString);
        settings.GuidRepresentation = GuidRepresentation.Standard;

        // Now create the MongoClient using the configured settings.
        _client = new MongoClient(settings);
        
        _database = _client.GetDatabase(appSettings.MongoDbDatabaseName);
        // CreateIndexes();
    }
    
    public IMongoClient Client => _client;
    public IMongoCollection<LaborRateEntity> LabourRates => _database.GetCollection<LaborRateEntity>("LabourRates");
    public IMongoCollection<ServiceCategoryEntity> ServiceCategories => _database.GetCollection<ServiceCategoryEntity>("ServiceCategories");
    public IMongoCollection<ServiceEntity> Services => _database.GetCollection<ServiceEntity>("Services");
    public IMongoCollection<MaterialEntity> Materials => _database.GetCollection<MaterialEntity>("Materials");
    public IMongoCollection<ServiceBundleEntity> ServiceBundles => _database.GetCollection<ServiceBundleEntity>("ServiceBundles");
    public IMongoCollection<WarrantyEntity> Warranties => _database.GetCollection<WarrantyEntity>("Warranties");
    public IMongoCollection<TaxRateEntity> TaxRates => _database.GetCollection<TaxRateEntity>("TaxRates");
}