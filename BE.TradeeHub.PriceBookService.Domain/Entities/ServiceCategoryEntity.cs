using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

/// <summary>
/// Top level service category which can hold multiple services or other service categories as sub categories
/// </summary>
public class ServiceCategoryEntity : AuditableEntity, IOwnedEntity
{
    [BsonId] public ObjectId Id { get; set; }

    public ObjectId? ParentServiceCategoryId { get; set; }

    /// <summary>
    /// The name of the service category
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The description of the service category
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Multiple Images of the service category
    /// </summary>
    public List<ImageEntity>? Images { get; set; }

    /// <summary>
    /// Service categories that are sub-categories of this service category
    /// </summary>
    public List<ObjectId>? ServiceCategoryIds { get; set; }

    /// <summary>
    /// Services that are part of this service category
    /// </summary>
    public List<ObjectId>? ServiceIds { get; set; } // each service can have sub-services aka another ServiceEntity

    public ServiceCategoryEntity()
    {
    }

    public ServiceCategoryEntity(string name, string? description, Guid userOwnerId, Guid createdById,
        ObjectId? parentServiceCategoryId)
    {
        Name = name;
        Description = description;
        UserOwnerId = userOwnerId;
        CreatedById = createdById;
        ParentServiceCategoryId = parentServiceCategoryId;
        Images = new List<ImageEntity>();
        CreatedAt = DateTime.UtcNow;
    }
}