using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;
using HotChocolate.Types.Relay;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Requests;

public class AddWarrantyRequest : IAddWarrantyRequest
{
    [ID]
    public ObjectId? ParentServiceCategoryId { get; set; }
    public string Name { get; set; }
    public string? WarrantyType { get; set; }
    public string? Description { get; set; }
    public string Terms { get; set; } 
    public WarrantyDurationRequest WarrantyDuration { get; set; } 
    public decimal? Price { get; set; }
}