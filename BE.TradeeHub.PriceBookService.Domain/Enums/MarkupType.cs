using System.ComponentModel;

namespace BE.TradeeHub.PriceBookService.Domain.Enums;

public enum MarkupType
{
    [Description("Fixed")]
    Fixed,
    [Description("Percentage")]
    Percentage,
}