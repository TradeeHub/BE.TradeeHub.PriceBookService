using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Application.Requests;

public class AddServiceBundleRequest : AddServiceRequest
{
    public required ObjectId ServiceId { get; set; }
}