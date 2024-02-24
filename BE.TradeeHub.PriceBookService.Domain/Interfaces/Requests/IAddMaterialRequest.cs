using BE.TradeeHub.PriceBookService.Domain.Entities;
using HotChocolate.Types;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;

public interface IAddMaterialRequest
{
    public string Name { get; }
    public IEnumerable<ObjectId>? ServiceIds { get; }
    public string? Description { get; }
    public string? Identifier { get; }
    public MarkupEntity? Markup { get; }
    public decimal Cost { get; }
    public decimal Price { get; }
    public string UnitType { get; }
    public IEnumerable<IFile>? Images { get; set; }
    public IEnumerable<string>? OnlineMaterialUrls { get; }
    public IEnumerable<PricingTierEntity>? PricingTiers { get; }
}