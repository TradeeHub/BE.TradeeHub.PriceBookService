using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests.Updates;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Requests.Update;

public class UpdateMaterialRequest : IUpdateMaterialRequest
{
    [ID]
    public ObjectId Id { get; set; }
    public string? Name { get; set; }
    [ID] public ObjectId? ParentServiceCategoryId { get; set; }
    public string? Description { get; set; }
    public string? Identifier { get; set; }
    public bool? UsePriceRange { get; set; }
    public bool? Taxable { get; set; }
    public bool? AllowOnlineBooking { get; set; }
    public decimal? OnlinePrice { get; set; }
    public decimal? Cost { get; set; }
    public decimal? Price { get; set; }
    public string? UnitType { get; set; }
    public string? Vendor { get; set; }
    public IEnumerable<PricingTierEntity>? PricingTiers { get; set; }
    public string? S3KeyToDelete { get; set; }
    public IFile? NewImage { get; set; }
}