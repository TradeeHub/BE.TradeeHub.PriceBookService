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

// This is a simple data loader that will go get the array of materials given the ids example I got 1 service it take the array of ids and get them all in 1 call 
// the grouped data loader I put above imagine we got 10 services being pulled same time and each one has many materials  this would end up making 10 calls to the db while the grouped one will make them all in 1 call
// [DataLoader]
// internal static async Task<IReadOnlyDictionary<ObjectId, MaterialEntity>> GetMaterialByIdAsync(
//     IReadOnlyList<ObjectId> materialIds, 
//     IMongoCollection<MaterialEntity> materials, 
//     CancellationToken cancellationToken)
// {
//     var filter = Builders<MaterialEntity>.Filter.In(m => m.Id, materialIds);
//     var materialList = await materials.Find(filter).ToListAsync(cancellationToken);
//
//     // Group the materials by each material ID and ensure it's read-only
//     var materialDictionary = materialList.ToDictionary(material => material.Id, material => material);
//
//     return materialDictionary;
// }