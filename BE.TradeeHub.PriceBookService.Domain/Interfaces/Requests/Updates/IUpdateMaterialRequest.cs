using BE.TradeeHub.PriceBookService.Domain.Entities;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests.Updates;

public interface IUpdateMaterialRequest
{
    [ID]
    public ObjectId Id { get; }
    public string? Name { get; }
    [ID] public ObjectId? ParentServiceCategoryId { get; }
    public string? Description { get; }
    public string? Identifier { get; }
    public bool? UsePriceRange { get; }
    public bool? Taxable { get; }
    public bool? AllowOnlineBooking { get; }
    public decimal? OnlinePrice { get; }
    public decimal? Cost { get; }
    public decimal? Price { get; }
    public string? UnitType { get; }
    public string? Vendor { get; }
    public IEnumerable<PricingTierEntity>? PricingTiers { get; }
    public string? S3KeyToDelete { get; set; }
    public IFile? NewImage { get; set; }
}