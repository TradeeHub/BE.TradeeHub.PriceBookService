using BE.TradeeHub.PriceBookService.Domain.Enums;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;

namespace BE.TradeeHub.PriceBookService.Domain.Requests;

public class MarkupRequest : IMarkupRequest
{
    public MarkupType Type { get; set; }
    public decimal Value { get; set; }
}