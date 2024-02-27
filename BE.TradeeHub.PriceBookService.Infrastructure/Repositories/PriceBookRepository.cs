using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;
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
}