using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Nodes;

[Node]
[ExtendObjectType(typeof(MaterialEntity))]
public static class MaterialNode
{
    public static async Task<List<ServiceEntity>> GetServices([Parent] MaterialEntity material,
        IServicesGroupedByIdDataLoader servicesGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (material.ServiceIds == null || material.ServiceIds.Count == 0)
        {
            return [];
        }

        var serviceGroups = await servicesGroupedByIdDataLoader.LoadAsync(material.ServiceIds, ctx);

        var services = serviceGroups.SelectMany(group => group).ToList();

        return services;
    }
    
    [DataLoader]
    internal static async Task<ILookup<ObjectId, MaterialEntity>> GetMaterialsGroupedByIdAsync(
        IReadOnlyList<ObjectId> materialIds,
        IMongoCollection<MaterialEntity> materials,
        CancellationToken cancellationToken)
    {
        var filter = Builders<MaterialEntity>.Filter.In(m => m.Id, materialIds);
        var materialList = await materials.Find(filter).ToListAsync(cancellationToken);

        return materialList.ToLookup(material => material.Id);
    }
}