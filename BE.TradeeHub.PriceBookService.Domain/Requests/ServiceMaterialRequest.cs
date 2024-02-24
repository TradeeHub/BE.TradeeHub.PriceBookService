using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Requests;

public class ServiceMaterialRequest : IServiceMaterialRequest
{
    public ObjectId MaterialId { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? Ratio { get; set; }
    public List<RangeTierUnitRequest>? Ranges { get; set; }
}