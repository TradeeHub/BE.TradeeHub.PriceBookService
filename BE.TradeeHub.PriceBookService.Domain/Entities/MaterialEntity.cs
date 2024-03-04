using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

/// <summary>
/// Material that can be used in a service
/// </summary>
public class MaterialEntity : AuditableEntity, IOwnedEntity
{
    [BsonId] public ObjectId Id { get; set; }

    /// <summary>
    /// This enforces only certain cervices to show this option if null it's a global option
    /// </summary>
    public ObjectId? ParentServiceCategoryId { get; set; }

    /// <summary>
    /// The Name of the Material
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The Description of the Material
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Unique identifier for the material that can be used to identify the material in third party
    /// </summary>
    public string? Identifier { get; set; }

    /// <summary>
    /// If markup is not null then the price will be calculated based on the markup
    /// </summary>
    public MarkupEntity? Markup { get; set; }

    /// <summary>
    /// If true it means it will use PricingTiers
    /// </summary>
    public bool UsePriceRange { get; set; }

    /// <summary>
    /// The tax will be applied to the material on the quote/job
    /// </summary>
    public bool Taxable { get; set; }

    /// <summary>
    /// Allows the material to be booked online
    /// </summary>
    public bool AllowOnlineBooking { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal? OnlinePrice { get; set; }

    /// <summary>
    /// The cost of the material (the amount I paid for the material)
    /// </summary>
    public decimal? Cost { get; set; }

    /// <summary>
    /// The price of the material I will be charging the customer
    /// </summary>
    public decimal? Price { get; set; }

    /// <summary>
    /// Unit of the material for example sqm, m, kg, etc
    /// </summary>
    public string UnitType { get; set; }

    /// <summary>
    /// Multiple Images of the material
    /// </summary>
    public List<ImageEntity>? Images { get; set; }

    /// <summary>
    /// Links to the online material store where the material can be purchased
    /// </summary>
    public List<string>? OnlineMaterialUrls { get; set; }

    /// <summary>
    /// Price tiers for the material based on the quantity of the material example 1-10, 11-20, 21-30 each tier will have a different price
    /// </summary>
    public List<PricingTierEntity>? PricingTiers { get; set; }

    public MaterialEntity()
    {
    }

    public MaterialEntity(IAddMaterialRequest addRequest, IUserContext userContext)
    {
        Name = addRequest.Name.Trim();
        ParentServiceCategoryId = addRequest.ParentServiceCategoryId;
        Description = addRequest.Description?.Trim();
        Identifier = addRequest.Identifier?.Trim();
        Markup = addRequest.Markup;
        Taxable = addRequest.Taxable;
        AllowOnlineBooking = addRequest.AllowOnlineBooking;
        OnlinePrice = addRequest is { UsePriceRange: false, AllowOnlineBooking: true } ? addRequest.OnlinePrice : null;
        UsePriceRange = addRequest.UsePriceRange;
        Cost = !addRequest.UsePriceRange ? addRequest.Cost : null;
        Price = !addRequest.UsePriceRange ? addRequest.Price : null;
        UnitType = addRequest.UnitType.Trim();
        Images = new List<ImageEntity>();
        OnlineMaterialUrls = addRequest.OnlineMaterialUrls?.ToList();
        PricingTiers = addRequest.UsePriceRange ? addRequest.PricingTiers?.ToList() : null;
        UserOwnerId = userContext.UserId;
        CreatedById = userContext.UserId;
        CreatedAt = DateTime.UtcNow;
    }
}