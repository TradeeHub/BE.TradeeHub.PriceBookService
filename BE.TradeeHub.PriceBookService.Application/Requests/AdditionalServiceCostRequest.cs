using BE.TradeeHub.PriceBookService.Domain.Enums;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Application.Requests;

public class AdditionalServiceCostRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Cost { get; set; }
    public TaxType? TaxRate { get; set; }
    public ObjectId? TaxRateId { get; set; }
}