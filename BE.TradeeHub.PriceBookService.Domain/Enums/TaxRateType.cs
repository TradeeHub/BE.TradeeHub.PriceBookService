using System.ComponentModel;

namespace BE.TradeeHub.PriceBookService.Domain.Enums;

public enum TaxRateType
{
    [Description("Inclusive of Tax")]
    Inclusive,
    [Description("Exempt from Tax")]
    Except,
    [Description("Specific Tax Rate")]
    SpecificRate,
}