using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Responses;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;

public interface IPriceBookRepository
{
    Task<ServiceCategoryEntity> CreateServiceCategoryAsync(ServiceCategoryEntity serviceCategory, CancellationToken ctx);
    Task<(OperationResult,ServiceCategoryEntity?)> DeleteServiceCategoryAsync(IUserContext userContext, ObjectId id, CancellationToken ctx);
    Task<LaborRateEntity> CreateLabourRateAsync(LaborRateEntity laborRate, CancellationToken ctx);
    Task<ServiceEntity> CreateServiceAsync(ServiceEntity service, CancellationToken ctx);
    Task<ServiceBundleEntity> CreateServiceBundleAsync(ServiceBundleEntity serviceBundle, CancellationToken ctx);
    Task<MaterialEntity> CreateMaterialAsync(MaterialEntity material, CancellationToken ctx);
    Task<TaxRateEntity> CreateTaxRateAsync(TaxRateEntity taxRate, CancellationToken ctx);
    Task<WarrantyEntity> CreateWarrantyAsync(WarrantyEntity warranty, CancellationToken ctx);
    Task<IList<ServiceCategoryEntity>> GetAllServiceCategoriesAsync(IUserContext userContext, BsonDocument projection, CancellationToken ctx);
}