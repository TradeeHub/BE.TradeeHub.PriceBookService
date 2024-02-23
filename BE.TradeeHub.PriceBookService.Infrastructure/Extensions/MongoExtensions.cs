using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Infrastructure.Extensions;

public static class MongoDbContextExtensions
{
    public static void EnsureIndexesCreated(this IMongoDbContext dbContext)
    {
        var labourRatesIndexKeys = Builders<LaborRateEntity>.IndexKeys.Ascending(lr => lr.UserOwnerId);
        var labourRatesIndexModel = new CreateIndexModel<LaborRateEntity>(labourRatesIndexKeys);
        dbContext.LabourRates.Indexes.CreateOne(labourRatesIndexModel);
        
        var serviceCategoriesIndexKeys = Builders<ServiceCategoryEntity>.IndexKeys.Ascending(sc => sc.UserOwnerId);
        var serviceCategoriesIndexModel = new CreateIndexModel<ServiceCategoryEntity>(serviceCategoriesIndexKeys);
        dbContext.ServiceCategories.Indexes.CreateOne(serviceCategoriesIndexModel);
        
        var servicesIndexKeys = Builders<ServiceEntity>.IndexKeys.Ascending(s => s.UserOwnerId);
        var servicesIndexModel = new CreateIndexModel<ServiceEntity>(servicesIndexKeys);
        dbContext.Services.Indexes.CreateOne(servicesIndexModel);
        
        var materialsIndexKeys = Builders<MaterialEntity>.IndexKeys.Ascending(m => m.UserOwnerId);
        var materialsIndexModel = new CreateIndexModel<MaterialEntity>(materialsIndexKeys);
        dbContext.Materials.Indexes.CreateOne(materialsIndexModel);
        
        var serviceBundlesIndexKeys = Builders<ServiceBundleEntity>.IndexKeys.Ascending(sb => sb.UserOwnerId);
        var serviceBundlesIndexModel = new CreateIndexModel<ServiceBundleEntity>(serviceBundlesIndexKeys);
        dbContext.ServiceBundles.Indexes.CreateOne(serviceBundlesIndexModel);
        
            
        var warrantiesIndexKeys = Builders<WarrantyEntity>.IndexKeys.Ascending(w => w.UserOwnerId);
        var warrantiesIndexModel = new CreateIndexModel<WarrantyEntity>(warrantiesIndexKeys);
        dbContext.Warranties.Indexes.CreateOne(warrantiesIndexModel);
        
            
        var taxRatesIndexKeys = Builders<TaxRateEntity>.IndexKeys.Ascending(tr => tr.UserOwnerId);
        var taxRatesIndexModel = new CreateIndexModel<TaxRateEntity>(taxRatesIndexKeys);
        dbContext.TaxRates.Indexes.CreateOne(taxRatesIndexModel);
    }
}