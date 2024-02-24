namespace BE.TradeeHub.PriceBookService.Domain.Interfaces;

public interface IUserContext
{
    public Guid UserId { get; }
    public string Name { get; }
    public string Email { get; }
    public string CompanyName { get; }
    public string LocationLat { get; }
    public string LocationLng { get; }
    public string Country { get; }
    public string CountryCode { get; }
    public string CallingCode { get; }
}