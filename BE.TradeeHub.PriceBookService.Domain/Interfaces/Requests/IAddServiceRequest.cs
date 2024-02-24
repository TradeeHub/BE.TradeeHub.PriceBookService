using BE.TradeeHub.PriceBookService.Domain.Enums;
using BE.TradeeHub.PriceBookService.Domain.Requests;
using HotChocolate.Types;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;

public interface IAddServiceRequest
{
    public string Name { get; }
    public decimal? Unit { get; }
    public string? UnitType { get; }
    public ServiceCreationType ServiceCreationType { get; }
    public bool UseCalculatedPrice { get; }
    public ServiceDurationRequest? Duration { get; }
    public decimal Cost { get; }
    public decimal Price { get; }
    public string? Description { get; }
    public IEnumerable<IFile>? Images { get; }
    public bool AllowOnlineBooking { get; }
    public ObjectId ServiceCategoryId { get; }
    public IEnumerable<ServiceMaterialRequest>? Materials { get; }
    public IEnumerable<ServiceLabourRequest>? LaborRates { get; }
    public ObjectId TaxRateId { get; }
    public MarkupRequest? Markup { get; }
    public IEnumerable<AdditionalServiceCostRequest>? AdditionalCosts { get; }
    public IEnumerable<ObjectId>? WarrantyIds { get; }
}