using System.Text.RegularExpressions;
using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Requests.Search;
using HotChocolate.Authorization;
using HotChocolate.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Queries;

[QueryType]
public static class MaterialQueries
{
    [Authorize]
    [UsePaging(MaxPageSize = 100)]
    [UseSorting]
    [UseFiltering]
    public static IExecutable<MaterialEntity> GetMaterials( 
        [Service] IMongoCollection<MaterialEntity> collection,
        [Service] UserContext userContext,
        SearchMaterialRequest request)
    {
        var filters = new List<FilterDefinition<MaterialEntity>>
        {
            Builders<MaterialEntity>.Filter.Eq(x => x.UserOwnerId, userContext.UserId)
        };

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            var nameFilter = Builders<MaterialEntity>.Filter.Regex(x => x.Name,
                new BsonRegularExpression($"{Regex.Escape(request.Name)}", "i"));
            filters.Add(nameFilter);
        }

        if (request.ParentServiceCategoryId.HasValue)
        {
            var parentServiceCategoryIdFilter = Builders<MaterialEntity>.Filter.Eq(x => x.ParentServiceCategoryId,
                request.ParentServiceCategoryId.Value);
            filters.Add(parentServiceCategoryIdFilter);
        }

        if (!string.IsNullOrWhiteSpace(request.Identifier))
        {
            var identifierFilter = Builders<MaterialEntity>.Filter.Regex(x => x.Identifier,
                new BsonRegularExpression($"{Regex.Escape(request.Identifier)}", "i"));
            filters.Add(identifierFilter);
        }

        if (request.Taxable.HasValue)
        {
            var taxableFilter = Builders<MaterialEntity>.Filter.Eq(x => x.Taxable, request.Taxable.Value);
            filters.Add(taxableFilter);
        }

        if (request.AllowOnlineBooking.HasValue)
        {
            var allowOnlineBookingFilter =
                Builders<MaterialEntity>.Filter.Eq(x => x.AllowOnlineBooking, request.AllowOnlineBooking.Value);
            filters.Add(allowOnlineBookingFilter);
        }

        if (request.OnlinePrice.HasValue)
        {
            var onlinePriceFilter = Builders<MaterialEntity>.Filter.Regex(x => x.OnlinePrice.ToString(),
                new BsonRegularExpression($"{Regex.Escape(request.OnlinePrice.Value.ToString())}", "i"));
            filters.Add(onlinePriceFilter);
        }

        if (request.Cost.HasValue)
        {
            var costFilter = Builders<MaterialEntity>.Filter.Regex(x => x.Cost.ToString(),
                new BsonRegularExpression($"{Regex.Escape(request.Cost.Value.ToString())}", "i"));
            filters.Add(costFilter);
        }

        if (request.Price.HasValue)
        {
            var priceFilter = Builders<MaterialEntity>.Filter.Regex(x => x.Price.ToString(),
                new BsonRegularExpression($"{Regex.Escape(request.Price.Value.ToString())}", "i"));
            filters.Add(priceFilter);
        }

        if (!string.IsNullOrWhiteSpace(request.UnitType))
        {
            var unitTypeFilter = Builders<MaterialEntity>.Filter.Eq(x => x.UnitType, request.UnitType);
            filters.Add(unitTypeFilter);
        }

        var combinedFilter = Builders<MaterialEntity>.Filter.And(filters);

        var query = collection.Find(combinedFilter);
        var executableQuery = query.AsExecutable();

        return executableQuery;
    }
}