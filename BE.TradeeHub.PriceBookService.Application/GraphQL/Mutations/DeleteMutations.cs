using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Services;
using HotChocolate.Authorization;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Mutations;

[MutationType]
public class DeleteMutations
{
    [NodeResolver]
    [Authorize]
    public async Task<IOperationResult> DeleteServiceCategoryAsync([Service] IPriceBookService priceBookService, [Service] UserContext userContext,[ID] ObjectId id, CancellationToken ctx)
    {
        return await priceBookService.DeleteServiceCategoryAsync(userContext, id, ctx);
    }
    
    [NodeResolver]
    [Authorize]
    public async Task<IOperationResult> DeleteMaterialAsync([Service] IPriceBookService priceBookService, [Service] UserContext userContext,[ID] ObjectId id, CancellationToken ctx)
    {
        return await priceBookService.DeleteMaterialAsync(userContext, id, ctx);
    }
}

