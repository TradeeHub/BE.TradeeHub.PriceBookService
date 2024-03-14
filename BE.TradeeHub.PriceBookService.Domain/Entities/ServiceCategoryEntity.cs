using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;
using HotChocolate.Types.Relay;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

/// <summary>
/// Top level service category which can hold multiple services or other service categories as sub categories
/// </summary>
public class ServiceCategoryEntity : AuditableEntity, IOwnedEntity
{
    [BsonId] public ObjectId Id { get; set; }

    [ID]
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
    public List<ObjectId> ServiceCategoryIds { get; set; } = [];

    /// <summary>
    /// Services that are part of this service category
    /// </summary>
    public List<ObjectId> ServiceIds { get; set; } = [];
    
    public ServiceCategoryEntity()
    {
    }
    
    public ServiceCategoryEntity(IAddNewServiceCategoryRequest addRequest, IUserContext userContext)
    {
        Name = addRequest.Name;
        Description = addRequest.Description;
        UserOwnerId = userContext.UserId;
        CreatedById = userContext.UserId;
        ParentServiceCategoryId = addRequest.ParentServiceCategoryId;
        Images = new List<ImageEntity>();
        CreatedAt = DateTime.UtcNow;
        ModifiedAt = DateTime.UtcNow;
    }
}