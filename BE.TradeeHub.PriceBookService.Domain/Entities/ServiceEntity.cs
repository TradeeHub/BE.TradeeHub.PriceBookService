using BE.TradeeHub.PriceBookService.Domain.Enums;
using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using HotChocolate.Types.Relay;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

/// <summary>
/// Top level service entity that will be used to define the service and the cost and price of the service
/// Example: Tiling service, Boiler service, Thermostat installation etc
/// Example: Bathroom tiling, Kitchen tiling, Floor tiling etc
/// Example: Installation of bathroom suite, Installation of kitchen suite, Installation of boiler etc
/// </summary>
public class ServiceEntity : AuditableEntity, IOwnedEntity
{
    [ID] [BsonId] public ObjectId Id { get; set; }

    /// <summary>
    /// The name of the service
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The quantity for a given service
    /// Example: Install 2 thermostats, or remove 10sqm tiles, or service the boiler aka 1 boiler
    /// </summary>
    public decimal? Unit { get; set; }

    /// <summary>
    /// The unit type for the service
    /// Example: Each, sqm, m, m3, kilo etc
    /// </summary>
    public string? UnitType { get; set; }

    /// <summary>
    /// The type of service creation you want to use
    /// Example: Fixed, Dynamic
    /// Fixed: The service will have a fixed price and cost example 1 install of a boiler £500
    /// Dynamic: Install floor tiles the quantity will depend on the sqm of the floor of each customer
    /// and when the customer has given the sqm I then cal pass the amount and generate a service cost and price
    /// </summary>
    public ServiceCreationType ServiceCreationType { get; set; }

    /// <summary>
    /// Enforced the service to use the calculated price and cost instead of manually setting the price and cost
    /// </summary>
    public bool UseCalculatedPrice { get; set; }

    /// <summary>
    /// The duration a given service will take
    /// </summary>
    public ServiceDurationEntity? Duration { get; set; }

    /// <summary>
    /// The cost of the service that I will encounter as a business to provide the service
    /// </summary>
    public decimal Cost { get; set; }

    /// <summary>
    /// The price of the service that I will charge the customer
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Description of the service
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// List of images for the service
    /// </summary>
    public List<ImageEntity>? Images { get; set; }

    /// <summary>
    /// Allows the service to be booked online
    /// </summary>
    public bool AllowOnlineBooking { get; set; }

    /// <summary>
    /// The category of which the service belongs to will mapped to the ServiceCategoryEntity via graphql
    /// </summary>
    public ObjectId ServiceCategory { get; set; }

    /// <summary>
    /// All materials that could be used in the service
    /// </summary>
    public List<ServiceMaterialEntity>? Materials { get; set; } = [];

    /// <summary>
    /// The Labour rates for a given service
    /// </summary>
    public List<ServiceLabourEntity>? LaborRates { get; set; } = [];

    /// <summary>
    /// The tax rate id that will map to the TaxRateEntity via graphql
    /// </summary>
    public ObjectId TaxRateId { get; set; }

    /// <summary>
    /// The markup that will be applied to the service if any
    /// </summary>
    public MarkupEntity? Markup { get; set; }

    /// <summary>
    /// Additional costs that can be applied to the service
    /// </summary>
    public List<AdditionalServiceCostEntity>? AdditionalCosts { get; set; }

    /// <summary>
    /// Warranties that can be applied to the service
    /// </summary>
    public List<ObjectId>? Warranties { get; set; }

    /// <summary>
    /// The Service bundles that can be applied to the service
    /// </summary>
    public List<ObjectId>? Bundles { get; set; }

    public ServiceEntity()
    {
    }

    public ServiceEntity(string name, decimal? unit, string? unitType, ServiceCreationType serviceCreationType,
        bool useCalculatedPrice, ServiceDurationEntity? duration, decimal cost, decimal price, string? description,
        bool allowOnlineBooking, ObjectId serviceCategory, List<ServiceMaterialEntity>? materials,
        List<ServiceLabourEntity>? laborRates, ObjectId taxRateId, MarkupEntity? markup,
        List<AdditionalServiceCostEntity>? additionalCosts,
        List<ObjectId>? warranties,
        Guid userOwnerId, Guid createdBy)
    {
        Name = name;
        Unit = unit;
        UnitType = unitType;
        ServiceCreationType = serviceCreationType;
        UseCalculatedPrice = useCalculatedPrice;
        Duration = duration;
        Cost = cost;
        Price = price;
        Description = description;
        Images = new List<ImageEntity>();
        AllowOnlineBooking = allowOnlineBooking;
        ServiceCategory = serviceCategory;
        Materials = materials;
        LaborRates = laborRates;
        TaxRateId = taxRateId;
        Markup = markup;
        AdditionalCosts = additionalCosts;
        Warranties = warranties;
        Bundles = null;
        UserOwnerId = userOwnerId;
        CreatedBy = createdBy;
        CreatedAt = DateTime.UtcNow;
    }
}