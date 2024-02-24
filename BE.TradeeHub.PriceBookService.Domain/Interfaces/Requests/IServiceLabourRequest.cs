using BE.TradeeHub.PriceBookService.Domain.Requests;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;

public interface IServiceLabourRequest
{
    public ObjectId LabourRateId { get; }
    public decimal? Quantity { get; }
    public decimal? Ratio { get; }
    public IEnumerable<RangeTierUnitRequest>? Ranges { get; }
}