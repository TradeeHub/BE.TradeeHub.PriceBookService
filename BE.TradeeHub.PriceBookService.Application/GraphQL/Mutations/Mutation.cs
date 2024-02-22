using BE.TradeeHub.PriceBookService.Application.Interfaces;
using BE.TradeeHub.PriceBookService.Application.Requests;
using BE.TradeeHub.PriceBookService.Domain.Entities;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Mutations;

public class Mutation
{
    public async Task<ServiceCategoryEntity> AddNewServiceCategoryAsync([Service] IPriceBookService priceBookService, [Service] UserContext userContext, AddNewServiceCategoryRequest request, CancellationToken ctx)
    {
        return await priceBookService.AddNewServiceCategoryAsync(userContext, request, ctx);
    }
}