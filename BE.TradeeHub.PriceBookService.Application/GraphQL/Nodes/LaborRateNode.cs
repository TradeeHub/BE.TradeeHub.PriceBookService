using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Nodes;

[Node]
[ExtendObjectType(typeof(LaborRateEntity))]
public static class LaborRateNode
{
    public static async Task<List<ServiceEntity>> GetServices([Parent] LaborRateEntity laborRate,
        IServicesGroupedByIdDataLoader servicesGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (laborRate.ServiceIds == null || laborRate.ServiceIds.Count == 0)
        {
            return [];
        }

        var serviceGroups = await servicesGroupedByIdDataLoader.LoadAsync(laborRate.ServiceIds, ctx);

        var services = serviceGroups.SelectMany(group => group).ToList();

        return services;
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