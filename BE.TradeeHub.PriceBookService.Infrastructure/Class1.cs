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
    
    public IMongoClient Client => _client; // Expose the MongoClient for external use
    // public IMongoCollection<CustomerEntity> Customers => _database.GetCollection<CustomerEntity>("Customers");
    // public IMongoCollection<CustomerReferenceNumberEntity> CustomerReferenceNumber => _database.GetCollection<CustomerReferenceNumberEntity>("Crns");
    // public IMongoCollection<PropertyEntity> Properties => _database.GetCollection<PropertyEntity>("Properties");
    // public IMongoCollection<CommentEntity> Comments => _database.GetCollection<CommentEntity>("Comments");
    // public IMongoCollection<ExternalReferenceEntity> ExternalReferences => _database.GetCollection<ExternalReferenceEntity>("ExternalReferences");
    
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