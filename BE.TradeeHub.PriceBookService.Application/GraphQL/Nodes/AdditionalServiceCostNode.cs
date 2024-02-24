using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Nodes;

[ExtendObjectType(typeof(AdditionalServiceCostEntity))]
public static class AdditionalServiceCostNode
{
    public static async Task<TaxRateEntity?> GetTaxRate([Parent] AdditionalServiceCostEntity additionalServiceCost,
        ITaxRateGroupedByIdDataLoader taxRateGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (additionalServiceCost.TaxRateId  == null || additionalServiceCost.TaxRateId == ObjectId.Empty)
        {
            return null;
        }

        var taxRate = await taxRateGroupedByIdDataLoader.LoadAsync(additionalServiceCost.TaxRateId.Value, ctx);

        return taxRate.FirstOrDefault();
    }
}