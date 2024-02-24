using BE.TradeeHub.PriceBookService.Domain.Requests;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;

public interface IAddLaborRateRequest
{
    public string Name { get; }
    public string? Description { get; }
    public string? RateType { get; }
    public decimal Cost { get; }
    public decimal Price { get; }
    public IEnumerable<ObjectId>? ServiceIds { get; }
    public IEnumerable<PricingTierRequest>? PricingTiers { get; }
}