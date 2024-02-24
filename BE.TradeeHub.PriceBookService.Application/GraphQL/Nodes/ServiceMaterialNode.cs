using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Nodes;


[ExtendObjectType(typeof(ServiceMaterialEntity), IgnoreProperties = ["MaterialId"])]
public static class ServiceMaterialNode
{
    public static async Task<MaterialEntity?> GetMaterial([Parent] ServiceMaterialEntity serviceMaterial,
        IMaterialsGroupedByIdDataLoader materialsGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (serviceMaterial.MaterialId == ObjectId.Empty)
        {
            return null;
        }
        
        var material = await materialsGroupedByIdDataLoader.LoadAsync(serviceMaterial.MaterialId, ctx);
    
        return material.FirstOrDefault();
    }
}