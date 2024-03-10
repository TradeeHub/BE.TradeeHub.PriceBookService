using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Services;
using BE.TradeeHub.PriceBookService.Domain.Requests.Update;
using BE.TradeeHub.PriceBookService.Domain.Responses;
using HotChocolate.Authorization;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Mutations;

[MutationType]
public class UpdateMutations
{
    [Authorize]
    public async Task<OperationResult<ServiceCategoryEntity?>> UpdateServiceCategoryAsync([Service] IPriceBookService priceBookService,
        [Service] UserContext userContext, UpdateServiceCategoryRequest request, CancellationToken ctx)
    {
        return await priceBookService.UpdateServiceCategoryAsync(userContext, request, ctx);
    }
}