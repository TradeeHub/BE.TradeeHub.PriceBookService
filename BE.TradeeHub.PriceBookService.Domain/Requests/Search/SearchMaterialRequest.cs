using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Requests.Search;

public class SearchMaterialRequest
{
    public string? Name { get; set; }
    public ObjectId? ParentServiceCategoryId { get; set; }
    public string? Identifier { get; set; }
    public bool? Taxable { get; set; }
    public bool? AllowOnlineBooking { get; set; }
    public decimal? OnlinePrice { get; set; }
    public decimal? Cost { get; set; }
    public decimal? Price { get; set; }
    public string? UnitType { get; set; }
}