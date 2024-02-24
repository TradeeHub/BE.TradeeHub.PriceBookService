using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;

namespace BE.TradeeHub.PriceBookService.Domain.Requests;

public class AddTaxRateRequest : IAddTaxRateRequest
{
    public required string Name { get; set; }
    public required string? Description { get; set; }
    public decimal PercentageRate { get; set; }
}