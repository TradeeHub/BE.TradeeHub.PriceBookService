using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;

public interface IMongoDbContext
{
    IMongoClient Client { get; }
    IMongoCollection<LaborRateEntity> LabourRates { get; }
    IMongoCollection<ServiceCategoryEntity> ServiceCategories { get; }
    IMongoCollection<ServiceEntity> Services { get; }
    IMongoCollection<ServiceBundleEntity> ServiceBundles { get; }
    IMongoCollection<MaterialEntity> Materials { get; }
    IMongoCollection<TaxRateEntity> TaxRates { get; }
    IMongoCollection<WarrantyEntity> Warranties { get; }
}