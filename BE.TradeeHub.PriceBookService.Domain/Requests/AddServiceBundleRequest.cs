using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Requests;

public class AddServiceBundleRequest : AddServiceRequest, IAddServiceBundleRequest
{
    public required ObjectId ServiceId { get; set; }
}