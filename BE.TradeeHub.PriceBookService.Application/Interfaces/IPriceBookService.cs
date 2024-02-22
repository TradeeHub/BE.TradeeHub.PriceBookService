using BE.TradeeHub.PriceBookService.Application.Requests;
using BE.TradeeHub.PriceBookService.Domain.Entities;

namespace BE.TradeeHub.PriceBookService.Application.Interfaces;

public interface IPriceBookService
{
    Task<ServiceCategoryEntity> AddNewServiceCategoryAsync(UserContext userContext, AddNewServiceCategoryRequest request, CancellationToken ctx);
    Task<LaborRateEntity> AddLaborRateAsync(UserContext userContext, AddLaborRateRequest request, CancellationToken ctx);
    Task<ServiceEntity> AddServiceAsync(UserContext userContext, AddServiceRequest request, CancellationToken ctx);
    Task<ServiceBundleEntity> AddServiceBundleAsync(UserContext userContext, AddServiceBundleRequest request, CancellationToken ctx);
    Task<MaterialEntity> AddMaterialAsync(UserContext userContext, AddMaterialRequest request, CancellationToken ctx);
    Task<TaxRateEntity> AddTaxRateAsync(UserContext userContext, AddTaxRateRequest request, CancellationToken ctx);
    Task<WarrantyEntity> AddWarrantyAsync(UserContext userContext, AddWarrantyRequest request, CancellationToken ctx);
}