using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Nodes;

[Node]
[ExtendObjectType(typeof(MaterialEntity))]
public static class MaterialNode
{
    public static async Task<ServiceCategoryEntity?> GetServices([Parent] MaterialEntity material,
        IServiceCategoryGroupedByIdDataLoader serviceCategoryGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (material.ParentServiceCategoryId == null || material.ParentServiceCategoryId == ObjectId.Empty)
        {
            return null;
        }

        var serviceCategories = await serviceCategoryGroupedByIdDataLoader.LoadAsync(material.ParentServiceCategoryId.Value, ctx);

        return serviceCategories.FirstOrDefault();
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