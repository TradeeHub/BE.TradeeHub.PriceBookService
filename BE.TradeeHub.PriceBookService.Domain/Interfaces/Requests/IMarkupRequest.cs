using BE.TradeeHub.PriceBookService.Domain.Enums;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;

public interface IMarkupRequest
{
    public MarkupType Type { get; }
    public decimal Value { get; }
}