using BE.TradeeHub.PriceBookService.Domain.Enums;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

/// <summary>
/// This is where the additional cost of a service is defined that are outside the service material or labor cost but are still part of the service cost
/// Example: Transportation cost, disposal cost, etc.
/// </summary>
public class AdditionalServiceCostEntity
{
    /// <summary>
    /// Name of the additional cost
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Description of the additional cost
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Cost of the additional cost
    /// </summary>
    public decimal Cost { get; set; }

    /// <summary>
    /// Tax rate of the additional cost
    /// </summary>
    public TaxRateType? TaxRateType { get; set; }

    /// <summary>
    /// Tax rate id reference of the additional cost
    /// </summary>
    public ObjectId? TaxRateId { get; set; }
    
    public AdditionalServiceCostEntity()
    {
    }
    
    public AdditionalServiceCostEntity (IAdditionalServiceCostRequest addRequest)
    {
        Name = addRequest.Name;
        Description = addRequest.Description;
        Cost = addRequest.Cost;
        TaxRateType = addRequest.TaxRateType;
        TaxRateId = addRequest.TaxRateId;
    }
}