using BE.TradeeHub.PriceBookService.Application.Requests;
using BE.TradeeHub.PriceBookService.Domain.Entities;

namespace BE.TradeeHub.PriceBookService.Application.Interfaces;

public interface IPriceBookService
{
    Task<ServiceCategoryEntity> AddNewServiceCategoryAsync(UserContext userContext, AddNewServiceCategoryRequest request, CancellationToken ctx);
}