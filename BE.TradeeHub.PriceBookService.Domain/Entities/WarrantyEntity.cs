using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

/// <summary>
/// Warranty entity that could be applied to a service
/// </summary>
public class WarrantyEntity : AuditableEntity, IOwnedEntity
{
    [BsonId] public ObjectId Id { get; set; }

    /// <summary>
    /// The services that the warranty can be applied to if empty then the warranty can be applied to all services
    /// </summary>
    public List<ObjectId>? ServiceIds { get; set; }

    /// <summary>
    /// The name of the warranty
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Warranty Type
    /// </summary>
    public string? WarrantyType { get; set; }

    /// <summary>
    /// Warranty Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Warranty Terms
    /// </summary>
    public string Terms { get; set; }

    /// <summary>
    /// Warranty Duration
    /// </summary>
    public WarrantyDurationEntity WarrantyDuration { get; set; }

    /// <summary>
    /// Warranty price
    /// </summary>
    public decimal? Price { get; set; }
    
    public WarrantyEntity()
    {
    }
    
    public WarrantyEntity (IAddWarrantyRequest addRequest, IUserContext userContext)
    {
        ServiceIds = addRequest.ServiceIds?.ToList();
        Name = addRequest.Name;
        WarrantyType = addRequest.WarrantyType;
        Description = addRequest.Description;
        Terms = addRequest.Terms;
        WarrantyDuration = new WarrantyDurationEntity(addRequest.WarrantyDuration);
        Price = addRequest.Price;
        UserOwnerId = userContext.UserId;
        CreatedById = userContext.UserId;
        CreatedAt = DateTime.UtcNow;
    }
}