using BE.TradeeHub.PriceBookService.Domain.Enums;
using BE.TradeeHub.PriceBookService.Domain.Requests;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;

public interface IServiceDurationRequest
{
    public DurationType Type { get; }
    public decimal DurationRangeFrom { get; }
    public decimal? DurationRangeTo { get; }  
    public decimal? Ratio { get; }
    public IEnumerable<RangeTierUnitRequest>? Ranges { get; }
}