using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;
using HotChocolate.Types.Relay;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Requests;

public class AddLaborRateRequest : IAddLaborRateRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? RateType { get; set; }
    public bool UsePriceRange { get; set;  }
    public decimal? Cost { get; set; }
    public decimal? Price { get; set; }
    [ID]
    public ObjectId? ParentServiceCategoryId  { get; set; }
    public IEnumerable<PricingTierRequest>? PricingTiers { get; set; }
}