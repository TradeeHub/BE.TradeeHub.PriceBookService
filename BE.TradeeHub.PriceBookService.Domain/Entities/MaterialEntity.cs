using HotChocolate.Types.Relay;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

/// <summary>
/// Material that can be used in a service
/// </summary>
public class MaterialEntity : AuditableEntity
{
    [ID] [BsonId] public ObjectId Id { get; set; }

    /// <summary>
    /// This enforces only certain cervices to show this option if null it's a global option
    /// </summary>
    public List<ObjectId>? Services { get; set; }

    /// <summary>
    /// The Name of the Material
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The Description of the Material
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Unique identifier for the material that can be used to identify the material in third party
    /// </summary>
    public string? Identifier { get; set; }

    /// <summary>
    /// If markup is not null then the price will be calculated based on the markup
    /// </summary>
    public MarkupEntity? Markup { get; set; }

    /// <summary>
    /// The cost of the material (the amount I paid for the material)
    /// </summary>
    public decimal Cost { get; set; }

    /// <summary>
    /// The price of the material I will be charging the customer
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Unit of the material for example sqm, m, kg, etc
    /// </summary>
    public required string UnitType { get; set; }

    /// <summary>
    /// Multiple Images of the material
    /// </summary>
    public List<string>? ImageUrl { get; set; }

    /// <summary>
    /// Links to the online material store where the material can be purchased
    /// </summary>
    public List<string>? OnlineMaterialUrl { get; set; }

    /// <summary>
    /// Price tiers for the material based on the quantity of the material example 1-10, 11-20, 21-30 each tier will have a different price
    /// </summary>
    public List<PricingTierEntity>? PricingTiers { get; set; }
}