﻿using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Nodes;

[Node]
[ExtendObjectType(typeof(TaxRateEntity))]
public static class TaxRateNode
{
    [DataLoader]
    internal static async Task<ILookup<ObjectId, TaxRateEntity>> GetTaxRateGroupedByIdAsync(
        IReadOnlyList<ObjectId> taxRateIds,
        IMongoCollection<TaxRateEntity> taxRates,
        CancellationToken cancellationToken)
    {
        var filter = Builders<TaxRateEntity>.Filter.In(m => m.Id, taxRateIds);
        var taxRateList = await taxRates.Find(filter).ToListAsync(cancellationToken);

        return taxRateList.ToLookup(taxRate => taxRate.Id);
    }
}   
