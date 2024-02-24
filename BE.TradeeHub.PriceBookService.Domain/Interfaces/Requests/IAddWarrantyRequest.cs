using BE.TradeeHub.PriceBookService.Domain.Requests;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;

public interface IAddWarrantyRequest
{
    public IEnumerable<ObjectId>? ServiceIds{ get; }
    public string Name { get; }
    public string? WarrantyType { get; }
    public string? Description { get; }
    public string Terms { get; }
    public WarrantyDurationRequest WarrantyDuration { get; }
    public decimal? Price { get; }
}