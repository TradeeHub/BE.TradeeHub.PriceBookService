namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;

public interface IAddTaxRateRequest
{
    public string Name { get; }
    public string? Description { get; }
    public decimal PercentageRate { get; }
}