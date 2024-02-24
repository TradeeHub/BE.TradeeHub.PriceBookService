using BE.TradeeHub.PriceBookService.Domain.Enums;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;

namespace BE.TradeeHub.PriceBookService.Domain.Requests;

public class WarrantyDurationRequest : IWarrantyDurationRequest
{
    public WarrantyDurationType DurationType { get; set; }
    public int Duration { get; set; }
}