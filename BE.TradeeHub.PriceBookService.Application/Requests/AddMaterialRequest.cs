using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Application.Requests;

public class AddMaterialRequest
{
    public required string Name { get; set; }
    public List<ObjectId>? ServiceIds { get; set; }
    public string? Description { get; set; }
    public string? Identifier { get; set; }
    public MarkupEntity? Markup { get; set; }
    public decimal Cost { get; set; }
    public decimal Price { get; set; }
    public required string UnitType { get; set; }
    public IEnumerable<IFile>? Images { get; set; }
    public List<string>? OnlineMaterialUrls { get; set; }
    public List<PricingTierEntity>? PricingTiers { get; set; }
}