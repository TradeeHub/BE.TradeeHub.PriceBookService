using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests.Updates;
using BE.TradeeHub.PriceBookService.Domain.Requests;
using BE.TradeeHub.PriceBookService.Domain.Requests.Update;
using BE.TradeeHub.PriceBookService.Domain.Responses;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Services;

public interface IPriceBookService
{
    Task<IList<ServiceCategoryEntity>> GetAllServiceCategoriesAsync(IUserContext userContext, BsonDocument projection, CancellationToken ctx);
    Task<ServiceCategoryEntity> AddNewServiceCategoryAsync(IUserContext userContext, AddNewServiceCategoryRequest request, CancellationToken ctx);
    Task<OperationResult> DeleteServiceCategoryAsync(IUserContext userContext, ObjectId id, CancellationToken ctx);
    Task<OperationResult> DeleteMaterialAsync(IUserContext userContext, ObjectId id, CancellationToken ctx);

    Task<OperationResult<ServiceCategoryEntity?>> UpdateServiceCategoryAsync(IUserContext userContext, UpdateServiceCategoryRequest request, CancellationToken ctx);
    Task<OperationResult<MaterialEntity?>> UpdateMaterialAsync(IUserContext userContext, UpdateMaterialRequest request, CancellationToken ctx);
    Task<LaborRateEntity> AddLaborRateAsync(IUserContext userContext, AddLaborRateRequest request, CancellationToken ctx);
    Task<ServiceEntity> AddServiceAsync(IUserContext userContext, AddServiceRequest request, CancellationToken ctx);
    Task<ServiceBundleEntity> AddServiceBundleAsync(IUserContext userContext, AddServiceBundleRequest request, CancellationToken ctx);
    Task<MaterialEntity> AddMaterialAsync(IUserContext userContext, AddMaterialRequest request, CancellationToken ctx);
    Task<TaxRateEntity> AddTaxRateAsync(IUserContext userContext, AddTaxRateRequest request, CancellationToken ctx);
    Task<WarrantyEntity> AddWarrantyAsync(IUserContext userContext, AddWarrantyRequest request, CancellationToken ctx);
}