using System.ComponentModel;

namespace BE.TradeeHub.PriceBookService.Domain.Enums;

public enum DurationType
{
    [Description("Minutes")]
    Minutes,
    [Description("Hours")]
    Hours,
    [Description("Days")]
    Days,
    [Description("Weeks")]
    Weeks,
    [Description("Months")]
    Months,
}