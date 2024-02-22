using MongoDB.Driver.Core.Misc;

namespace BE.TradeeHub.PriceBookService.Application.Requests;

public class PricingTierRequest
{
    public required Range<decimal> UnitRange { get; set; }
    public decimal? Cost { get; set; }
    public decimal Price { get; set; }
}