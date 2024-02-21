using Amazon.S3;
using Amazon.S3.Model;
using BE.TradeeHub.PriceBookService.Application.Interfaces;
using BE.TradeeHub.PriceBookService.Application.Requests;
using BE.TradeeHub.PriceBookService.Domain.Entities;
using HotChocolate.Resolvers;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Mutations;

public class Mutation
{
    public async Task<ServiceCategoryEntity> AddNewServiceCategoryAsync([Service] IPriceBookService priceBookService, [Service] UserContext userContext, AddNewServiceCategoryRequest request, CancellationToken ctx)
    {
        return await priceBookService.AddNewServiceCategoryAsync(userContext, request, ctx);
    }
}