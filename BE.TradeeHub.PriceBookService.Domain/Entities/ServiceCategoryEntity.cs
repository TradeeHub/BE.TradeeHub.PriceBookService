using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using HotChocolate.Types.Relay;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

/// <summary>
/// Top level service category which can hold multiple services or other service categories as sub categories
/// </summary>
public class ServiceCategoryEntity : AuditableEntity, IOwnedEntity
{
    [ID] [BsonId] public ObjectId Id { get; set; }

    public ObjectId? ParentServiceCategory { get; set; }

    /// <summary>
    /// The name of the service category
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The description of the service category
    /// </summary>
    public string? Description { get; set; }

    public List<string>? ImagesS3Keys { get; set; }

    /// <summary>
    /// Multiple Images of the service category
    /// </summary>
    public List<string>? Images { get; set; }

    /// <summary>
    /// Service categories that are sub-categories of this service category
    /// </summary>
    public List<ObjectId>? ServiceCategories { get; set; }

    /// <summary>
    /// Services that are part of this service category
    /// </summary>
    public List<ObjectId>? Services { get; set; } // each service can have sub-services aka another ServiceEntity

    public ServiceCategoryEntity()
    {
    }

    public ServiceCategoryEntity(string name, string? description, Guid userOwnerId, Guid createdBy,
        ObjectId? parentServiceCategory)
    {
        Name = name;
        Description = description;
        UserOwnerId = userOwnerId;
        CreatedBy = createdBy;
        ParentServiceCategory = parentServiceCategory;
        ImagesS3Keys = new List<string>();
        Images = new List<string>();
        CreatedAt = DateTime.UtcNow;
    }
}