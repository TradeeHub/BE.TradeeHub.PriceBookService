using MongoDB.Driver.Core.Misc;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

/// <summary>
/// Pricing Tier used for when there is a range of pricing for a unit like 1-10, 11-20, 21-30 different prices for each range
/// which could be used for labour, materials, etc for companies that offer better deals the more you buy
/// </summary>
public class PricingTierEntity
{
    /// <summary>
    /// The range of the pricing tier for the unit proportion for example:
    /// If the range of the quantity/unit 1-10 then I would have certain costs and pricing if the quantity/unit is 11-20 then I would have different costs and pricing
    /// </summary>
    public required Range<decimal> UnitRange { get; set; }

    /// <summary>
    /// Cost of the Entity for the Unite Range
    /// </summary>
    public decimal? Cost { get; set; }

    /// <summary>
    /// Price of the entity for the Unit Range
    /// </summary>
    public decimal Price { get; set; }
}