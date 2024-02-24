using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Nodes;

[Node]
[ExtendObjectType(typeof(ServiceBundleEntity), IgnoreProperties = ["UserOwnerId", "CreatedBy", "ModifiedBy", "TaxRateId"])]
public static class ServiceBundleNode
{
    public static async Task<ServiceEntity?> GetService([Parent] ServiceBundleEntity service,
        IServicesGroupedByIdDataLoader servicesGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (service.ServiceCategoryId == ObjectId.Empty)
        {
            return null;
        }

        var services = await servicesGroupedByIdDataLoader.LoadAsync(service.ServiceCategoryId, ctx);

        return services.FirstOrDefault();
    }
    
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
        if (service.ServiceCategoryId == ObjectId.Empty)
        {
            return null;
        }

        var serviceCategory = await serviceCategoryGroupedByIdDataLoader.LoadAsync(service.ServiceCategoryId, ctx);

        return serviceCategory.FirstOrDefault();
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