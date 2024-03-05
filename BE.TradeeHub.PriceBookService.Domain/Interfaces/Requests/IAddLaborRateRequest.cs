using BE.TradeeHub.PriceBookService.Domain.Requests;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;

public interface IAddLaborRateRequest
{
    public string Name { get; }
    public string? Description { get; }
    public string? RateType { get; }
    public bool UsePriceRange { get; }
    public decimal? Cost { get; }
    public decimal? Price { get; }
    public ObjectId? ParentServiceCategoryId  { get; }
    public IEnumerable<PricingTierRequest>? PricingTiers { get; }
}