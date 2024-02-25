using BE.TradeeHub.PriceBookService.Domain.SubgraphEntities;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

public class ImageEntity
{
    /// <summary>
    /// AWS Cloud Front Image Url
    /// </summary>
    public string Url { get; set; }
    
    /// <summary>
    /// AWS S3 Key
    /// </summary>
    public string S3Key { get; set; }
    
    /// <summary>
    /// The name of the image which it had when getting uploaded.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// The size of the image in bytes.
    /// </summary>
    public long? ByteSize { get; set; }
    
    /// <summary>
    /// The content type of the image if it is available.
    /// </summary>
    public string? ContentType { get; set; }
    
    /// <summary>
    /// A description of the image.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Date and time when the image was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// CreatedBy is the unique identifier of the User who created the entity.
    /// </summary>
    public Guid CreatedById { get; set; }

    /// <summary>
    /// ModifiedAt is the date and time when the entity was last modified.
    /// If the entity has never been modified, it is null.
    /// </summary>
    public DateTime? ModifiedAt { get; set; }

    /// <summary>
    /// ModifiedBy is the unique identifier of the User who last modified the entity.
    /// If the entity has never been modified, it is null.
    /// </summary>
    public Guid? ModifiedById { get; set; }
    
    /// <summary>
    /// GraphQL Relay ID of the Creator that will be used to expose the entity to the GraphQL API.
    /// </summary>
    /// <returns></returns>
    public UserEntity Creator() => new UserEntity { Id = CreatedById };

    /// <summary>
    /// GraphQL Relay ID of the Modifier that will be used to expose the entity to the GraphQL API.
    /// </summary>
    /// <returns></returns>
    public UserEntity? Modifier() => ModifiedById.HasValue ? new UserEntity { Id = ModifiedById.Value } : null;
    
    public ImageEntity()
    {
    }
    
    public ImageEntity(string url, string s3Key, string name, long? byteSize, string? contentType, Guid createdById, string? description = null)
    {
        Url = url;
        S3Key = s3Key;
        Name = name;
        ByteSize = byteSize;
        ContentType = contentType;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        CreatedById = createdById;
    }
}