using MongoDB.Driver.Core.Misc;

namespace BE.TradeeHub.PriceBookService.Application.Requests;

public class RangeTierUnitRequest
{
    public decimal Quantity { get; set; }
    public required Range<decimal> Range { get; set; }
}