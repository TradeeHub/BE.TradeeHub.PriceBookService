using BE.TradeeHub.PriceBookService.Domain.Entities;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;

public interface IPriceBookRepository
{
    Task<ServiceCategoryEntity> CreateServiceCategory(ServiceCategoryEntity serviceCategory, CancellationToken cancellationToken);
    Task<LaborRateEntity> CreateLabourRate(LaborRateEntity laborRate, CancellationToken cancellationToken);
    Task<ServiceEntity> CreateService(ServiceEntity service, CancellationToken cancellationToken);
    Task<ServiceBundleEntity> CreateServiceBundle(ServiceBundleEntity serviceBundle, CancellationToken cancellationToken);
    Task<MaterialEntity> CreateMaterial(MaterialEntity material, CancellationToken cancellationToken);
    Task<TaxRateEntity> CreateTaxRate(TaxRateEntity taxRate, CancellationToken cancellationToken);
    Task<WarrantyEntity> CreateWarranty(WarrantyEntity warranty, CancellationToken cancellationToken);
}