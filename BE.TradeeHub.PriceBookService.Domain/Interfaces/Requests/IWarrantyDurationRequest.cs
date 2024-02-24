using BE.TradeeHub.PriceBookService.Domain.Enums;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;

public interface IWarrantyDurationRequest
{
    public WarrantyDurationType DurationType { get; }

    public int Duration { get; }
}