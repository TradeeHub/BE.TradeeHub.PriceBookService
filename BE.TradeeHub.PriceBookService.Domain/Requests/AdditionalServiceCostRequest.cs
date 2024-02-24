using BE.TradeeHub.PriceBookService.Domain.Enums;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Requests;

public class AdditionalServiceCostRequest : IAdditionalServiceCostRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Cost { get; set; }
    public TaxRateType? TaxRateType { get; set; }
    public ObjectId? TaxRateId { get; set; }
}