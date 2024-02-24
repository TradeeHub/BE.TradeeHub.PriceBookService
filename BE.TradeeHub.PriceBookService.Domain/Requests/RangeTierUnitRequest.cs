using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;
using MongoDB.Driver.Core.Misc;

namespace BE.TradeeHub.PriceBookService.Domain.Requests;

public class RangeTierUnitRequest : IRangeTierUnitRequest
{
    public decimal Quantity { get; set; }
    public Range<decimal> Range { get; set; }
}