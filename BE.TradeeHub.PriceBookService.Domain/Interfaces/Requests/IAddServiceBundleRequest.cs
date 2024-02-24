using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;

public interface IAddServiceBundleRequest : IAddServiceRequest
{
    public ObjectId ServiceId { get; }
}