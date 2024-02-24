using MongoDB.Driver.Core.Misc;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;

public interface IPricingTierRequest
{
    public Range<decimal> UnitRange { get; }
    public decimal? Cost { get; }
    public decimal Price { get; }
}