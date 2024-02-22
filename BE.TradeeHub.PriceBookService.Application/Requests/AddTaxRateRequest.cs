namespace BE.TradeeHub.PriceBookService.Application.Requests;

public class AddTaxRateRequest
{
    public required string Name { get; set; }
    public required string? Description { get; set; }
    public decimal PercentageRate { get; set; }
}