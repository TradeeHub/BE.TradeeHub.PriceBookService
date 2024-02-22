using BE.TradeeHub.PriceBookService.Domain.SubgraphEntities;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

/// <summary>
/// Used to track the creation and modification of entities and ownership.
/// </summary>
public abstract class AuditableEntity
{
    /// <summary>
    /// The unique identifier of the User who owns this entity.
    /// </summary>
    public Guid UserOwnerId { get; set; }

    /// <summary>
    /// CreatedAt is the date and time when the entity was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// CreatedBy is the unique identifier of the User who created the entity.
    /// </summary>
    public Guid CreatedBy { get; set; }

    /// <summary>
    /// ModifiedAt is the date and time when the entity was last modified.
    /// If the entity has never been modified, it is null.
    /// </summary>
    public DateTime? ModifiedAt { get; set; }

    /// <summary>
    /// ModifiedBy is the unique identifier of the User who last modified the entity.
    /// If the entity has never been modified, it is null.
    /// </summary>
    public Guid? ModifiedBy { get; set; }

    /// <summary>
    /// GraphQL Relay ID of the Owner that will be used to expose the entity to the GraphQL API.
    /// </summary>
    /// <returns></returns>
    public UserEntity Owner() => new UserEntity { Id = UserOwnerId };

    /// <summary>
    /// GraphQL Relay ID of the Creator that will be used to expose the entity to the GraphQL API.
    /// </summary>
    /// <returns></returns>
    public UserEntity Creator() => new UserEntity { Id = CreatedBy };

    /// <summary>
    /// GraphQL Relay ID of the Modifier that will be used to expose the entity to the GraphQL API.
    /// </summary>
    /// <returns></returns>
    public UserEntity? Modifier() => ModifiedBy.HasValue ? new UserEntity { Id = ModifiedBy.Value } : null;
}