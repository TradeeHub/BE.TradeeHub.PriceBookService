using BE.TradeeHub.PriceBookService.Domain.Enums;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;
using HotChocolate.Types;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Requests;

public class AddServiceRequest : IAddServiceRequest
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
    public ObjectId ServiceCategoryId { get; set; }
    public IEnumerable<ServiceMaterialRequest>? Materials { get; set; } = [];
    public IEnumerable<ServiceLabourRequest>? LaborRates { get; set; } = [];
    public ObjectId TaxRateId { get; set; }
    public MarkupRequest? Markup { get; set; }
    public IEnumerable<AdditionalServiceCostRequest>? AdditionalCosts { get; set; }
    public IEnumerable<ObjectId>? WarrantyIds { get; set; }
}