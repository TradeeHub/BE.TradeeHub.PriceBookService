using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;
using HotChocolate.Types;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Requests;

public class AddMaterialRequest : IAddMaterialRequest
{
    public required string Name { get; set; }
    public IEnumerable<ObjectId>? ServiceIds { get; set; }
    public string? Description { get; set; }
    public string? Identifier { get; set; }
    public MarkupEntity? Markup { get; set; }
    public decimal Cost { get; set; }
    public decimal Price { get; set; }
    public required string UnitType { get; set; }
    public IEnumerable<IFile>? Images { get; set; }
    public IEnumerable<string>? OnlineMaterialUrls { get; set; }
    public IEnumerable<PricingTierEntity>? PricingTiers { get; set; }
}