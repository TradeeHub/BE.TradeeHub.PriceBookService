using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Application.Requests;

public class AddWarrantyRequest
{
    public List<ObjectId>? Services { get; set; }
    public required string Name { get; set; }
    public string? WarrantyType { get; set; }
    public string? Description { get; set; }
    public required string Terms { get; set; }
    public required WarrantyDurationEntity WarrantyDuration { get; set; }
    public decimal? Price { get; set; }
}