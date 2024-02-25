using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Services;
using BE.TradeeHub.PriceBookService.Domain.Requests;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Mutations;

[MutationType]
public class Mutation
{
    public async Task<ServiceCategoryEntity> AddNewServiceCategoryAsync([Service] IPriceBookService priceBookService, [Service] UserContext userContext, AddNewServiceCategoryRequest request, CancellationToken ctx)
    {
        return await priceBookService.AddNewServiceCategoryAsync(userContext, request, ctx);
    }
    
    public async Task<LaborRateEntity> AddLaborRateAsync([Service] IPriceBookService priceBookService, [Service] UserContext userContext, AddLaborRateRequest request, CancellationToken ctx)
    {
        return await priceBookService.AddLaborRateAsync(userContext, request, ctx);
    }

    public async Task<ServiceEntity> AddServiceAsync([Service] IPriceBookService priceBookService, [Service] UserContext userContext, AddServiceRequest request, CancellationToken ctx)
    {
        return await priceBookService.AddServiceAsync(userContext, request, ctx);
    }

    public async Task<ServiceBundleEntity> AddServiceBundleAsync([Service] IPriceBookService priceBookService, [Service] UserContext userContext, AddServiceBundleRequest request, CancellationToken ctx)
    {
        return await priceBookService.AddServiceBundleAsync(userContext, request, ctx);
    }
    
    public async Task<MaterialEntity> AddMaterialAsync([Service] IPriceBookService priceBookService, [Service] UserContext userContext, AddMaterialRequest request, CancellationToken ctx)
    {
        return await priceBookService.AddMaterialAsync(userContext, request, ctx);
    }
    
    public async Task<TaxRateEntity> AddTaxRateAsync([Service] IPriceBookService priceBookService, [Service] UserContext userContext, AddTaxRateRequest request, CancellationToken ctx)
    {
        return await priceBookService.AddTaxRateAsync(userContext, request, ctx);
    }
    
    public async Task<WarrantyEntity> AddWarrantyAsync([Service] IPriceBookService priceBookService, [Service] UserContext userContext, AddWarrantyRequest request, CancellationToken ctx)
    {
        return await priceBookService.AddWarrantyAsync(userContext, request, ctx);
    }
}