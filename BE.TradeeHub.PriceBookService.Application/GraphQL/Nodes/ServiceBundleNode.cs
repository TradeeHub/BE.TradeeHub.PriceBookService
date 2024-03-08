using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Nodes;

[Node]
[ExtendObjectType(typeof(ServiceBundleEntity), IgnoreProperties = ["UserOwnerId", "CreatedById", "ModifiedById", "TaxRateId"])]
public static class ServiceBundleNode
{
    public static async Task<List<WarrantyEntity>> GetWarranties([Parent] ServiceBundleEntity service,
        IWarrantiesGroupedByIdDataLoader warrantyByIdDataLoader, CancellationToken ctx)
    {
        if (service.WarrantyIds == null || service.WarrantyIds.Count == 0)
        {
            return [];
        }

        var warrantyGroups = await warrantyByIdDataLoader.LoadAsync(service.WarrantyIds, ctx);

        var warranties = warrantyGroups.SelectMany(group => group).ToList();

        return warranties;
    }

    public static async Task<TaxRateEntity?> GetTaxRate([Parent] ServiceBundleEntity service,
        ITaxRateGroupedByIdDataLoader taxRateByIdDataLoader, CancellationToken ctx)
    {
        if (service.TaxRateId == null || service.TaxRateId == ObjectId.Empty)
        {
            return null;
        }

        var taxRate = await taxRateByIdDataLoader.LoadAsync(service.TaxRateId.Value, ctx);

        return taxRate.FirstOrDefault();
    }

    public static async Task<ServiceCategoryEntity?> GetServiceCategory([Parent] ServiceBundleEntity service,
        IServiceCategoryGroupedByIdDataLoader serviceCategoryGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (service.ParentServiceCategoryId == null || service.ParentServiceCategoryId == ObjectId.Empty)
        {
            return null;
        }        
        
        var serviceCategories = await serviceCategoryGroupedByIdDataLoader.LoadAsync(service.ParentServiceCategoryId.Value, ctx);

        return serviceCategories.FirstOrDefault();
    }

    [DataLoader]
    internal static async Task<ILookup<ObjectId, ServiceBundleEntity>> GetServiceBundleGroupedByIdAsync(
        IReadOnlyList<ObjectId> serviceBundleIds,
        IMongoCollection<ServiceBundleEntity> serviceBundles,
        CancellationToken cancellationToken)
    {
        var filter = Builders<ServiceBundleEntity>.Filter.In(m => m.Id, serviceBundleIds);
        var serviceBundleList = await serviceBundles.Find(filter).ToListAsync(cancellationToken);

        return serviceBundleList.ToLookup(serviceBundle => serviceBundle.Id);
    }
}