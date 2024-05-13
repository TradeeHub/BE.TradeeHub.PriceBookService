using BE.TradeeHub.PriceBookService.Domain.Entities;
using HotChocolate.Types;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;

public interface IAddMaterialRequest
{
    public string Name { get; }
    public ObjectId? ParentServiceCategoryId { get; }
    public string? Description { get; }
    public string? Identifier { get; }
    public bool UsePriceRange { get; set; }
    public bool Taxable { get; }
    public bool AllowOnlineBooking { get; }
    public decimal? OnlinePrice { get; }
    public decimal? Cost { get; }
    public decimal? Price { get; }
    public string UnitType { get; }
    public IEnumerable<IFile>? Images { get; set; }
    public string? Vendor { get; }
    public IEnumerable<PricingTierEntity>? PricingTiers { get; }
}