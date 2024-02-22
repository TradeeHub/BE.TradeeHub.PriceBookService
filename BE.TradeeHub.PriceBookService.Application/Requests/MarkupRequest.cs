using BE.TradeeHub.PriceBookService.Domain.Enums;

namespace BE.TradeeHub.PriceBookService.Application.Requests;

public class MarkupRequest
{
    public MarkupType Type { get; set; }
    public decimal Value { get; set; }
}