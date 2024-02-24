using BE.TradeeHub.PriceBookService.Domain.Enums;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;

namespace BE.TradeeHub.PriceBookService.Domain.Requests;

public class ServiceDurationRequest: IServiceDurationRequest
{
    public DurationType Type { get; set; }
    public decimal DurationRangeFrom { get; set; }
    public decimal? DurationRangeTo { get; set; }  
    public decimal? Ratio { get; set; }
    public IEnumerable<RangeTierUnitRequest>? Ranges { get; set; }
}