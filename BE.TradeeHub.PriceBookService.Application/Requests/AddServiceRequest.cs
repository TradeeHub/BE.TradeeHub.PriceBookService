using BE.TradeeHub.PriceBookService.Domain.Enums;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Application.Requests;

public class AddServiceRequest
{
    public required string Name { get; set; }
    public decimal? Unit { get; set; }
    public string? UnitType { get; set; }
    public required ServiceCreationType ServiceCreationType { get; set; }
    public bool UseCalculatedPrice { get; set; }
    public ServiceDurationRequest? Duration { get; set; }
    public decimal Cost { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public IEnumerable<IFile>? Images { get; set; }
    public bool AllowOnlineBooking { get; set; }
    public ObjectId ServiceCategory { get; set; }
    public List<ServiceMaterialRequest>? Materials { get; set; } = [];
    public List<ServiceLabourRequest>? LaborRates { get; set; } = [];
    public ObjectId TaxRateId { get; set; }
    public MarkupRequest? Markup { get; set; }
    public List<AdditionalServiceCostRequest>? AdditionalCosts { get; set; }
    public List<ObjectId>? Warranties { get; set; }
}