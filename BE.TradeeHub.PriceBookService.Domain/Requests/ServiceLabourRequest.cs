using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Requests;

public class ServiceLabourRequest : IServiceLabourRequest
{
    public ObjectId LabourRateId { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? Ratio { get; set; }
    public IEnumerable<RangeTierUnitRequest>? Ranges { get; set; } = [];
}