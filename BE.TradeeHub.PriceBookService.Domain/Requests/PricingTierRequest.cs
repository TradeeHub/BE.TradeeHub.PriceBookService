using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;
using MongoDB.Driver.Core.Misc;

namespace BE.TradeeHub.PriceBookService.Domain.Requests;

public class PricingTierRequest: IPricingTierRequest
{
    public required Range<decimal> UnitRange { get; set; }
    public decimal? Cost { get; set; }
    public decimal Price { get; set; }
}