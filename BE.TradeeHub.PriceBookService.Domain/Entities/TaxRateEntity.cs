using HotChocolate.Types.Relay;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

/// <summary>
/// The tax rate that can be applied to a service or material or labour
/// </summary>
public class TaxRateEntity : AuditableEntity
{
    [ID] [BsonId] public ObjectId Id { get; set; }

    /// <summary>
    /// The tax rate that can be applied to a service or material or labour
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The description of the tax rate
    /// </summary>
    public required string? Description { get; set; }

    /// <summary>
    /// The percentage of the tax rate
    /// </summary>
    public decimal PercentageRate { get; set; }
}