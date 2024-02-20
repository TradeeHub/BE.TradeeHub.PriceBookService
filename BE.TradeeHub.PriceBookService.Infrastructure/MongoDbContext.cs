using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Infrastructure;

public class MongoDbContext
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

    private void CreateIndexes()
    {
        // var customersCollection = _database.GetCollection<CustomerEntity>("Customers");
        // var customerIndexModel = new CreateIndexModel<CustomerEntity>(
        //     Builders<CustomerEntity>.IndexKeys.Ascending(customer => customer.UserOwnerId)); // Specify the field you want to index
        // customersCollection.Indexes.CreateOne(customerIndexModel);
        //
        // var propertiesCollection = _database.GetCollection<PropertyEntity>("Properties");
        // var propertyIndexModel = new CreateIndexModel<PropertyEntity>(
        //     Builders<PropertyEntity>.IndexKeys.Ascending(property => property.UserOwnerId)); // Specify the field you want to index
        // propertiesCollection.Indexes.CreateOne(propertyIndexModel);
        //
        // var commentsCollection = _database.GetCollection<CommentEntity>("Comments");
        // var commentIndexModel = new CreateIndexModel<CommentEntity>(
        //     Builders<CommentEntity>.IndexKeys
        //         .Ascending(comment => comment.CustomerId)
        //         .Ascending(comment => comment.UserOwnerId));
        //
        // commentsCollection.Indexes.CreateOne(commentIndexModel);
    }
}