using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Requests;

public class AddLaborRateRequest : IAddLaborRateRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? RateType { get; set; }
    public decimal Cost { get; set; }
    public decimal Price { get; set; }
    public IEnumerable<ObjectId>? ServiceIds { get; set; }
    public IEnumerable<PricingTierRequest>? PricingTiers { get; set; }
}