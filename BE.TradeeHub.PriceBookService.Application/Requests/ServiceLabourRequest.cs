using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Application.Requests;

public class ServiceLabourRequest
{
    public ObjectId LabourRateId { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? Ratio { get; set; }
    public List<RangeTierUnitRequest>? Ranges { get; set; }
}