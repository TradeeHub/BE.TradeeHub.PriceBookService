using MongoDB.Driver.Core.Misc;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;

public interface IRangeTierUnitRequest
{
    public decimal Quantity { get; }
    public Range<decimal> Range { get; }
}