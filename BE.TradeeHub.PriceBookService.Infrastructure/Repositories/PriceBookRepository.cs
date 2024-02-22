using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;

namespace BE.TradeeHub.PriceBookService.Infrastructure.Repositories;

public class PriceBookRepository : IPriceBookRepository
{
    private readonly MongoDbContext _dbContext;

    public PriceBookRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ServiceCategoryEntity> CreateServiceCategory(ServiceCategoryEntity serviceCategory, CancellationToken cancellationToken)
    {
        await _dbContext.ServiceCategories.InsertOneAsync(serviceCategory, cancellationToken: cancellationToken);
        return serviceCategory;
    }
}