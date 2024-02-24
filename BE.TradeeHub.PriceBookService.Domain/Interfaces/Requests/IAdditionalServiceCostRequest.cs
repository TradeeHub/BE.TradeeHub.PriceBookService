using BE.TradeeHub.PriceBookService.Domain.Enums;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;

public interface IAdditionalServiceCostRequest
{
    public string Name { get; }
    public string? Description { get; }
    public decimal Cost { get; }
    public TaxRateType? TaxRateType { get; }
    public ObjectId? TaxRateId { get; }
}