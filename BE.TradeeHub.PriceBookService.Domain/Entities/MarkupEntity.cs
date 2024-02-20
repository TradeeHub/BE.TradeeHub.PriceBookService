using BE.TradeeHub.PriceBookService.Domain.Enums;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

/// <summary>
/// If a markup is applied to a price, this entity is used to define the markup of the price
/// Example: If a markup of 10% is applied to a price, for a price of 100, the markup will be 10 so the total price will be 110
/// </summary>
public class MarkupEntity
{
    /// <summary>
    /// Markup type (Fixed or Percentage)
    /// </summary>
    public MarkupType Type { get; set; }

    /// <summary>
    /// The value of the markup
    /// </summary>
    public decimal Value { get; set; }
}