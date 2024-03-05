using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Nodes;

[Node]
[ExtendObjectType(typeof(LaborRateEntity))]
public static class LaborRateNode
{
    public static async Task<ServiceCategoryEntity?> GetServiceCategory([Parent] LaborRateEntity laborRate,
        IServiceCategoryGroupedByIdDataLoader serviceCategoryGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (laborRate.ParentServiceCategoryId == null || laborRate.ParentServiceCategoryId == ObjectId.Empty)
        {
            return null;
        }

        var serviceCategories = await serviceCategoryGroupedByIdDataLoader.LoadAsync(laborRate.ParentServiceCategoryId.Value, ctx);

        return serviceCategories.FirstOrDefault();
    }

    [DataLoader]
    internal static async Task<ILookup<ObjectId, LaborRateEntity>> GetLaborRatesGroupedByIdAsync(
        IReadOnlyList<ObjectId> laborRateIds,
        IMongoCollection<LaborRateEntity> laborRates,
        CancellationToken cancellationToken)
    {
        var filter = Builders<LaborRateEntity>.Filter.In(m => m.Id, laborRateIds);
        var laborRateList = await laborRates.Find(filter).ToListAsync(cancellationToken);

        return laborRateList.ToLookup(laborRate => laborRate.Id);
    }
}