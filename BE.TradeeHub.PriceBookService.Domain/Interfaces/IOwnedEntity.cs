using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces;

public interface IOwnedEntity
{
    ObjectId Id { get; set; }
    Guid UserOwnerId { get; set; }
}