using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Application.Requests;

public class AddLaborRateRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? RateType { get; set; }
    public decimal Cost { get; set; }
    public decimal Price { get; set; }
    public List<ObjectId>? Services { get; set; }
    public List<PricingTierRequest>? PricingTiers { get; set; }
}