using BE.TradeeHub.PriceBookService.Application.Requests;
using BE.TradeeHub.PriceBookService.Domain.Entities;
using MongoDB.Driver.Core.Misc;

namespace BE.TradeeHub.PriceBookService.Application.Mappings;

public static class Mappings
{
    public static WarrantyEntity ToWarrantyEntity(this AddWarrantyRequest request, Guid userOwnerId, Guid createdBy)
    {
        return new WarrantyEntity(request.Services, request.Name, request.WarrantyType, request.Description,
            request.Terms, request.WarrantyDuration, request.Price, userOwnerId, createdBy);
    }

    public static MaterialEntity ToMaterialEntity(this AddMaterialRequest request, Guid userOwnerId, Guid createdBy)
    {
        return new MaterialEntity(request.Name, request.ServiceIds, request.Description, request.Identifier, request.Markup, request.Cost,
            request.Price, request.UnitType, request.OnlineMaterialUrls, request.PricingTiers,
            userOwnerId, createdBy);
    }

    public static ServiceEntity ToServiceEntity(this AddServiceRequest request, Guid userOwnerId, Guid createdBy)
    {
        return new ServiceEntity(request.Name, request.Unit, request.UnitType, request.ServiceCreationType,
            request.UseCalculatedPrice, request.Duration?.ToServiceDurationEntity(), request.Cost, request.Price,
            request.Description, request.AllowOnlineBooking, request.ServiceCategory,
            request.Materials?.Select(m => m.ToServiceMaterialEntity()).ToList(),
            request.LaborRates?.Select(l => l.ToServiceLabourEntity()).ToList(),
            request.TaxRateId, request.Markup?.ToMarkupEntity(),
            request.AdditionalCosts?.Select(ac => ac.ToAdditionalServiceCostEntity()).ToList(), request.Warranties,
            userOwnerId, createdBy);
    }

    public static ServiceBundleEntity ToServiceBundleEntity(this AddServiceBundleRequest request, Guid userOwnerId,
        Guid createdBy)
    {
        return new ServiceBundleEntity
        (request.ServiceId, request.Name, request.Unit, request.UnitType, request.ServiceCreationType,
            request.UseCalculatedPrice, request.Duration?.ToServiceDurationEntity(), request.Cost,
            request.Price, request.Description, request.AllowOnlineBooking, request.ServiceCategory,
            request.Materials?.Select(m => m.ToServiceMaterialEntity()).ToList(),
            request.LaborRates?.Select(l => l.ToServiceLabourEntity()).ToList(), request.TaxRateId,
            request.Markup?.ToMarkupEntity(),
            request.AdditionalCosts?.Select(ac => ac.ToAdditionalServiceCostEntity()).ToList(), request.Warranties,
            userOwnerId, createdBy
        );
    }

    public static LaborRateEntity ToLaborRateEntity(this AddLaborRateRequest request, Guid userOwnerId, Guid createdBy)
    {
        return new LaborRateEntity(request.Name, request.Description, request.RateType, request.Cost,
            request.Price, request.Services, request.PricingTiers?.Select(pt => pt.ToPricingTierEntity()).ToList(),
            userOwnerId, createdBy
        );
    }

    public static ServiceCategoryEntity ToServiceCategoryEntity(this AddNewServiceCategoryRequest request,
        Guid userOwnerId, Guid createdBy)
    {
        return new ServiceCategoryEntity(request.Name, request.Description, userOwnerId, createdBy,
            request.ParentServiceCategory);
    }

    public static TaxRateEntity ToTaxRateEntity(this AddTaxRateRequest request, Guid userOwnerId, Guid createdBy)
    {
        return new TaxRateEntity(request.Name, request.Description, request.PercentageRate, userOwnerId, createdBy);
    }

    private static ServiceMaterialEntity ToServiceMaterialEntity(this ServiceMaterialRequest request)
    {
        return new ServiceMaterialEntity(request.MaterialId, request.Quantity, request.Ratio,
            request.Ranges?.Select(r => r.ToRangeTierUnitEntity()).ToList());
    }

    private static ServiceLabourEntity ToServiceLabourEntity(this ServiceLabourRequest request)
    {
        return new ServiceLabourEntity(request.LabourRateId, request.Quantity, request.Ratio,
            request.Ranges?.Select(r => r.ToRangeTierUnitEntity()).ToList());
    }

    private static RangeTierUnitEntity ToRangeTierUnitEntity(this RangeTierUnitRequest request)
    {
        return new RangeTierUnitEntity(request.Quantity, new Range<decimal>(request.Range.Min, request.Range.Max));
    }

    private static MarkupEntity ToMarkupEntity(this MarkupRequest request)
    {
        return new MarkupEntity(request.Type, request.Value);
    }

    private static AdditionalServiceCostEntity ToAdditionalServiceCostEntity(this AdditionalServiceCostRequest request)
    {
        return new AdditionalServiceCostEntity(request.Name, request.Description, request.Cost, request.TaxRate,
            request.TaxRateId);
    }

    private static ServiceDurationEntity ToServiceDurationEntity(this ServiceDurationRequest request)
    {
        return new ServiceDurationEntity(request.Type, request.DurationRangeFrom, request.DurationRangeTo,
            request.Ratio, request.Ranges?.Select(r => r.ToRangeTierUnitEntity()).ToList());
    }

    private static PricingTierEntity ToPricingTierEntity(this PricingTierRequest request)
    {
        return new PricingTierEntity(request.UnitRange, request.Cost, request.Price);
    }
}