using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Application.Requests;

public class ServiceMaterialRequest
{
    public ObjectId MaterialId { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? Ratio { get; set; }
    public List<RangeTierUnitRequest>? Ranges { get; set; }
}
