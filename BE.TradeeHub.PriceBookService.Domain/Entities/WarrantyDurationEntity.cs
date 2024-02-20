using BE.TradeeHub.PriceBookService.Domain.Enums;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

/// <summary>
/// The Warranty duration of a service
/// </summary>
public class WarrantyDurationEntity
{
    /// <summary>
    /// Duration type of the warranty which could be days, weeks, months, years, lifetime
    /// </summary>
    public WarrantyDurationType DurationType { get; set; }

    /// <summary>
    /// The duration of the warranty
    /// </summary>
    public int Duration { get; set; }
}