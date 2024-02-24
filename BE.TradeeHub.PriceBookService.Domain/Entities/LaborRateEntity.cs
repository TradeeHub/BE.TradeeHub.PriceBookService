using BE.TradeeHub.PriceBookService.Domain.Enums;
using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using HotChocolate.Types.Relay;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

/// <summary>
/// Labor rate used to define the cost and price of labor for a specific rate type
/// Example: hourly, daily, sqm, sqft, meter, etc so that I can charge the customer accordingly
/// Example: I can have a labor rate for painting which is hourly and another labor rate for tiling which is sqm etc...
/// </summary>
public class LaborRateEntity : AuditableEntity, IOwnedEntity
{
    [BsonId] public ObjectId Id { get; set; }

    /// <summary>
    /// Labor rate name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Labor rate description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Labor rate type which could be hourly, daily, sqm, sqft, meter, etc
    /// </summary>
    [BsonRepresentation(BsonType.String)]
    public string? RateType { get; set; }

    /// <summary>
    /// The cost that I will end up paying for the labor for the rate type
    /// </summary>
    public decimal Cost { get; set; }

    /// <summary>
    /// Labor rate price I need to charge the customer for the rate type to be in profit
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// This enforces only certain cervices to show this option if null it's a global option
    /// </summary>
    public List<ObjectId>? ServiceIds { get; set; }

    /// <summary>
    /// Pricing tiers for the labor rate which can very based on the quantity of the rate type if I have specific pricing for different quantities
    /// </summary>
    public List<PricingTierEntity>? PricingTiers { get; set; }
    
    public LaborRateEntity()
    {
    }
    
    public LaborRateEntity (string name, string? description, string? rateType, decimal cost, decimal price,List<ObjectId>? serviceIds, List<PricingTierEntity>? pricingTiers, Guid userOwnerId, Guid createdById)
    {
        Name = name;
        Description = description;
        RateType = rateType;
        Cost = cost;
        Price = price;
        ServiceIds = serviceIds;
        PricingTiers = pricingTiers;
        UserOwnerId = userOwnerId;
        CreatedById = createdById;
        CreatedAt = DateTime.UtcNow;
    }
}