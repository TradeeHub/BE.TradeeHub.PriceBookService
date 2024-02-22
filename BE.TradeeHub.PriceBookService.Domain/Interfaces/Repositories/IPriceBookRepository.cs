using BE.TradeeHub.PriceBookService.Domain.Entities;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;

public interface IPriceBookRepository
{
    Task<ServiceCategoryEntity> CreateServiceCategory(ServiceCategoryEntity serviceCategory, CancellationToken cancellationToken);
}