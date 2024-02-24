using BE.TradeeHub.PriceBookService.Domain.Requests;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;

public interface IServiceMaterialRequest
{
    public ObjectId MaterialId { get; }
    public decimal? Quantity { get; }
    public decimal? Ratio { get; }
    public List<RangeTierUnitRequest>? Ranges { get; }
}
