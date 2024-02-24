using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using HotChocolate.Types.Relay;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

/// <summary>
/// The tax rate that can be applied to a service or material or labour
/// </summary>
public class TaxRateEntity : AuditableEntity, IOwnedEntity
{
    [ID] [BsonId] public ObjectId Id { get; set; }

    /// <summary>
    /// The tax rate that can be applied to a service or material or labour
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The description of the tax rate
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The percentage of the tax rate
    /// </summary>
    public decimal PercentageRate { get; set; }

    public TaxRateEntity()
    {
        
    }
    public TaxRateEntity (string name, string? description, decimal percentageRate, Guid userOwnerId, Guid createdById)
    {
        Name = name;
        Description = description;
        PercentageRate = percentageRate;
        UserOwnerId = userOwnerId;
        CreatedById = createdById;
        CreatedAt = DateTime.UtcNow;
    }
}