using System.ComponentModel;

namespace BE.TradeeHub.PriceBookService.Domain.Enums;

public enum WarrantyDurationType
{
    [Description("Days")]
    Days,
    [Description("Weeks")]
    Weeks,
    [Description("Months")]
    Months,
    [Description("Years")]
    Years,
    [Description("Lifetime")]
    Lifetime,
}