using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;
using BE.TradeeHub.PriceBookService.Domain.Responses;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Infrastructure.Repositories;

public class PriceBookRepository : IPriceBookRepository
{
    private readonly IMongoDbContext _dbContext;

    public PriceBookRepository(IMongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ServiceCategoryEntity> CreateServiceCategoryAsync(ServiceCategoryEntity serviceCategory, CancellationToken cancellationToken)
    {
        await _dbContext.ServiceCategories.InsertOneAsync(serviceCategory, cancellationToken: cancellationToken);
        return serviceCategory;
    }
    
    public async Task<(OperationResult, ServiceCategoryEntity?)> DeleteServiceCategoryAsync(IUserContext userContext, ObjectId id, CancellationToken ctx)
    {
        var operationResult = new OperationResult();
        var serviceCategory = await _dbContext.ServiceCategories.Find(sc => sc.Id == id).FirstOrDefaultAsync(ctx);
        
        if (serviceCategory == null)
        {
            return (operationResult.AddError($"Service category with ID {id} not found."),serviceCategory);
        }
        
        await UnlinkEntitiesFromServiceCategoryAsync<ServiceCategoryEntity>(id, serviceCategory.Name, "service categories", ctx, operationResult);
        await UnlinkEntitiesFromServiceCategoryAsync<LaborRateEntity>(id, serviceCategory.Name, "labor rates", ctx, operationResult);
        await UnlinkEntitiesFromServiceCategoryAsync<MaterialEntity>(id, serviceCategory.Name, "materials", ctx, operationResult);
        await UnlinkEntitiesFromServiceCategoryAsync<ServiceEntity>(id, serviceCategory.Name, "services", ctx, operationResult);
        await UnlinkEntitiesFromServiceCategoryAsync<WarrantyEntity>(id, serviceCategory.Name, "warranties", ctx, operationResult);
        await UnlinkEntitiesFromServiceCategoryAsync<ServiceBundleEntity>(id, serviceCategory.Name, "service bundles", ctx, operationResult);

        var deleteResult = await _dbContext.ServiceCategories.DeleteOneAsync(sc => sc.Id == id, ctx);

        return deleteResult.IsAcknowledged switch
        {
            true when deleteResult.DeletedCount > 0 => (operationResult, serviceCategory),
            _ => (operationResult.AddError("Failed to delete the service category."),serviceCategory)
        };
    }
    
    public async Task<LaborRateEntity> CreateLabourRateAsync(LaborRateEntity laborRate, CancellationToken cancellationToken)
    {
        await _dbContext.LabourRates.InsertOneAsync(laborRate, cancellationToken: cancellationToken);
        return laborRate;
    }
    
    public async Task<ServiceEntity> CreateServiceAsync(ServiceEntity service, CancellationToken cancellationToken)
    {
        await _dbContext.Services.InsertOneAsync(service, cancellationToken: cancellationToken);
        return service;
    }
    
    public async Task<ServiceBundleEntity> CreateServiceBundleAsync(ServiceBundleEntity serviceBundle, CancellationToken cancellationToken)
    {
        await _dbContext.ServiceBundles.InsertOneAsync(serviceBundle, cancellationToken: cancellationToken);
        return serviceBundle;
    }
    
    public async Task<MaterialEntity> CreateMaterialAsync(MaterialEntity material, CancellationToken cancellationToken)
    {
        await _dbContext.Materials.InsertOneAsync(material, cancellationToken: cancellationToken);
        return material;
    }
    
    public async Task<TaxRateEntity> CreateTaxRateAsync(TaxRateEntity taxRate, CancellationToken cancellationToken)
    {
        await _dbContext.TaxRates.InsertOneAsync(taxRate, cancellationToken: cancellationToken);
        return taxRate;
    }
    
    public async Task<WarrantyEntity> CreateWarrantyAsync(WarrantyEntity warranty, CancellationToken ctx)
    {
        await _dbContext.Warranties.InsertOneAsync(warranty, cancellationToken: ctx);
        return warranty;
    }
    
    public async Task<IList<ServiceCategoryEntity>> GetAllServiceCategoriesAsync(
        IUserContext userContext, BsonDocument projection, CancellationToken ctx)
    {
        // Apply the projection in the MongoDB query
        var filter = Builders<ServiceCategoryEntity>.Filter.Eq(x => x.UserOwnerId, userContext.UserId);
        var sort = Builders<ServiceCategoryEntity>.Sort.Descending(x => x.ModifiedAt).Descending(x => x.CreatedAt);
        
        return await _dbContext.ServiceCategories
            .Find(filter)
            .Sort(sort)
            .Project<ServiceCategoryEntity>(projection)
            .ToListAsync(ctx);
    }
    
    private async Task UnlinkLaborRatesFromServiceCategoryAsync(ObjectId parentServiceCategoryId, string serviceCategoryName, CancellationToken ctx, OperationResult? operationResult = null)
    {
        var filter = Builders<LaborRateEntity>.Filter.Eq(lr => lr.ParentServiceCategoryId, parentServiceCategoryId);
        var update = Builders<LaborRateEntity>.Update.Set(lr => lr.ParentServiceCategoryId, null);
        var result = await _dbContext.LabourRates.UpdateManyAsync(filter, update, cancellationToken: ctx);

        if (result.IsAcknowledged && result.ModifiedCount > 0)
        {
            operationResult?.AddMessage($"{result.ModifiedCount} labor rates unlinked from service category {serviceCategoryName}.");
        }
        {
            operationResult?.AddMessage($"{result.ModifiedCount} labor rates unlinked from service category {serviceCategoryName}.");
        }
    }
    
    private async Task UnlinkServiceCategoriesFromServiceCategoryAsync(ObjectId parentServiceCategoryId, string serviceCategoryName, CancellationToken ctx, OperationResult? operationResult = null)
    {
        var filter = Builders<ServiceCategoryEntity>.Filter.Eq(lr => lr.ParentServiceCategoryId, parentServiceCategoryId);
        var update = Builders<ServiceCategoryEntity>.Update.Set(lr => lr.ParentServiceCategoryId, null);
        var result = await _dbContext.ServiceCategories.UpdateManyAsync(filter, update, cancellationToken: ctx);

        if (result.IsAcknowledged && result.ModifiedCount > 0)
        {
            operationResult?.AddMessage($"{result.ModifiedCount} service categories unlinked from service category {serviceCategoryName}.");
        }
        {
            operationResult?.AddMessage($"{result.ModifiedCount} service categories unlinked from service category {serviceCategoryName}.");
        }
    }
    
    private async Task UnlinkEntitiesFromServiceCategoryAsync<T>(ObjectId parentServiceCategoryId, string serviceCategoryName, string entityMessage, CancellationToken ctx, OperationResult? operationResult = null) where T : class
    {
        var collectionProperty = typeof(IMongoDbContext).GetProperties()
            .FirstOrDefault(p => p.PropertyType == typeof(IMongoCollection<T>));
        if (collectionProperty == null)
        {
            operationResult?.AddError($"No collection found for type {typeof(T).Name}.");
            return;
        }

        var collection = (IMongoCollection<T>)collectionProperty.GetValue(_dbContext)!;

        // Building filter and update definitions
        var filter = Builders<T>.Filter.Eq("ParentServiceCategoryId", parentServiceCategoryId);
        var update = Builders<T>.Update.Set("ParentServiceCategoryId", BsonNull.Value);

        var result = await collection.UpdateManyAsync(filter, update, cancellationToken: ctx);

        // Add operation result message
        if (result.IsAcknowledged)
        {
            var message = $"{result.ModifiedCount} {entityMessage} unlinked from service category {serviceCategoryName}.";
            operationResult?.AddMessage(message);
        }
        else
        {
            operationResult?.AddError("Operation not acknowledged by the database.");
        }
    }
}