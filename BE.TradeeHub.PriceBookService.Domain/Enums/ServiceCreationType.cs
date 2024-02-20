using System.ComponentModel;

namespace BE.TradeeHub.PriceBookService.Domain.Enums;

public enum ServiceCreationType
{
    [Description("Fixed")]
    Fixed,
    [Description("Dynamic")]
    Dynamic,
}