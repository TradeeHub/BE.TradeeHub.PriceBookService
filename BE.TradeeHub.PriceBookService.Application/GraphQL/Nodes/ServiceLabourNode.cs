using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Nodes;

[ExtendObjectType(typeof(ServiceLabourEntity), IgnoreProperties = ["LabourRateId"])]
public static class ServiceLabourNode
{
    public static async Task<LaborRateEntity?> GetLaborRate([Parent] ServiceLabourEntity serviceLabour,
        ILaborRatesGroupedByIdDataLoader laborRatesGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (serviceLabour.LabourRateId == ObjectId.Empty)
        {
            return null;
        }
        
        var laborRate = await laborRatesGroupedByIdDataLoader.LoadAsync(serviceLabour.LabourRateId, ctx);
    
        return laborRate.FirstOrDefault();
    }
}