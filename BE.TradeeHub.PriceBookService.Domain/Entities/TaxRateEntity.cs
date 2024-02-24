using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

/// <summary>
/// The tax rate that can be applied to a service or material or labour
/// </summary>
public class TaxRateEntity : AuditableEntity, IOwnedEntity
{
    [BsonId] public ObjectId Id { get; set; }

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
    public TaxRateEntity (IAddTaxRateRequest addRequest, IUserContext userContext)
    {
        Name = addRequest.Name;
        Description = addRequest.Description;
        PercentageRate = addRequest.PercentageRate;
        UserOwnerId = userContext.UserId;
        CreatedById = userContext.UserId;
        CreatedAt = DateTime.UtcNow;
    }
}