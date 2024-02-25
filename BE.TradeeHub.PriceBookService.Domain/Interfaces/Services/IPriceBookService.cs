using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Requests;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Services;

public interface IPriceBookService
{
    Task<ServiceCategoryEntity> AddNewServiceCategoryAsync(IUserContext userContext, AddNewServiceCategoryRequest request, CancellationToken ctx);
    Task<LaborRateEntity> AddLaborRateAsync(IUserContext userContext, AddLaborRateRequest request, CancellationToken ctx);
    Task<ServiceEntity> AddServiceAsync(IUserContext userContext, AddServiceRequest request, CancellationToken ctx);
    Task<ServiceBundleEntity> AddServiceBundleAsync(IUserContext userContext, AddServiceBundleRequest request, CancellationToken ctx);
    Task<MaterialEntity> AddMaterialAsync(IUserContext userContext, AddMaterialRequest request, CancellationToken ctx);
    Task<TaxRateEntity> AddTaxRateAsync(IUserContext userContext, AddTaxRateRequest request, CancellationToken ctx);
    Task<WarrantyEntity> AddWarrantyAsync(IUserContext userContext, AddWarrantyRequest request, CancellationToken ctx);
}