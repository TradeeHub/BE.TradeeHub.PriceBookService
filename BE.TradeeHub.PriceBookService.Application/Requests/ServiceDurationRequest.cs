using BE.TradeeHub.PriceBookService.Domain.Enums;

namespace BE.TradeeHub.PriceBookService.Application.Requests;

public class ServiceDurationRequest
{
    public DurationType Type { get; set; }
    public decimal DurationRangeFrom { get; set; }
    public decimal? DurationRangeTo { get; set; }  
    public decimal? Ratio { get; set; }
    public List<RangeTierUnitRequest>? Ranges { get; set; }
}