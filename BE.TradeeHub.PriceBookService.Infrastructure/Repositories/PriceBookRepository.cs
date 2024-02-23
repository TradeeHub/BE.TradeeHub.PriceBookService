using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;

namespace BE.TradeeHub.PriceBookService.Infrastructure.Repositories;

public class PriceBookRepository : IPriceBookRepository
{
    private readonly IMongoDbContext _dbContext;

    public PriceBookRepository(IMongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ServiceCategoryEntity> CreateServiceCategory(ServiceCategoryEntity serviceCategory, CancellationToken cancellationToken)
    {
        await _dbContext.ServiceCategories.InsertOneAsync(serviceCategory, cancellationToken: cancellationToken);
        return serviceCategory;
    }
    
    public async Task<LaborRateEntity> CreateLabourRate(LaborRateEntity laborRate, CancellationToken cancellationToken)
    {
        await _dbContext.LabourRates.InsertOneAsync(laborRate, cancellationToken: cancellationToken);
        return laborRate;
    }
    
    public async Task<ServiceEntity> CreateService(ServiceEntity service, CancellationToken cancellationToken)
    {
        await _dbContext.Services.InsertOneAsync(service, cancellationToken: cancellationToken);
        return service;
    }
    
    public async Task<ServiceBundleEntity> CreateServiceBundle(ServiceBundleEntity serviceBundle, CancellationToken cancellationToken)
    {
        await _dbContext.ServiceBundles.InsertOneAsync(serviceBundle, cancellationToken: cancellationToken);
        return serviceBundle;
    }
    
    public async Task<MaterialEntity> CreateMaterial(MaterialEntity material, CancellationToken cancellationToken)
    {
        await _dbContext.Materials.InsertOneAsync(material, cancellationToken: cancellationToken);
        return material;
    }
    
    public async Task<TaxRateEntity> CreateTaxRate(TaxRateEntity taxRate, CancellationToken cancellationToken)
    {
        await _dbContext.TaxRates.InsertOneAsync(taxRate, cancellationToken: cancellationToken);
        return taxRate;
    }
    
    public async Task<WarrantyEntity> CreateWarranty(WarrantyEntity warranty, CancellationToken cancellationToken)
    {
        await _dbContext.Warranties.InsertOneAsync(warranty, cancellationToken: cancellationToken);
        return warranty;
    }
}