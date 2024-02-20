using System.ComponentModel;

namespace BE.TradeeHub.PriceBookService.Domain.Enums;

public enum LaborRateType
{
    [Description("Hourly")]
    Hourly,
    [Description("Daily")]
    Daily,
}