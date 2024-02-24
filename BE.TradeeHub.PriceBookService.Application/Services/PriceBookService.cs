using Amazon.S3;
using BE.TradeeHub.PriceBookService.Application.Interfaces;
using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using Amazon.S3.Model;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;
using BE.TradeeHub.PriceBookService.Domain.Requests;

namespace BE.TradeeHub.PriceBookService.Application.Services;

public class PriceBookService : IPriceBookService
{
    private readonly IPriceBookRepository _priceBookRepository;
    private readonly IImageRepository _imageRepository;
    
    public PriceBookService(IImageRepository imageRepository, IPriceBookRepository priceBookRepository)
    {
        _imageRepository = imageRepository;
        _priceBookRepository = priceBookRepository;
    }

    public async Task<ServiceCategoryEntity> AddNewServiceCategoryAsync(UserContext userContext,
        AddNewServiceCategoryRequest request, CancellationToken ctx)
    {
        var newServiceCategory = new ServiceCategoryEntity(request, userContext);

        if (request.Images == null || !request.Images.Any())
            return await _priceBookRepository.CreateServiceCategory(newServiceCategory, ctx);

        var uploadTasks = request.Images
            .Select(image => _imageRepository.UploadImageAsync(image, userContext.UserId, "service-category", ctx)).ToList();
        var images = await Task.WhenAll(uploadTasks);

        foreach (var image in images)
        {
            newServiceCategory.Images?.Add(image);
        }

        return await _priceBookRepository.CreateServiceCategory(newServiceCategory, ctx);
    }

    public async Task<LaborRateEntity> AddLaborRateAsync(UserContext userContext, AddLaborRateRequest request,
        CancellationToken ctx)
    {
        var laborRateEntity = new LaborRateEntity(request, userContext);

        return await _priceBookRepository.CreateLabourRate(laborRateEntity, ctx);
    }

    public async Task<ServiceEntity> AddServiceAsync(UserContext userContext, AddServiceRequest request,
        CancellationToken ctx)
    {
        var serviceEntity = new ServiceEntity(request, userContext);

        if (request.Images == null || !request.Images.Any())
            return await _priceBookRepository.CreateService(serviceEntity, ctx);

        var uploadTasks = request.Images.Select(image => _imageRepository.UploadImageAsync(image, userContext.UserId, "services", ctx))
            .ToList();
        var images = await Task.WhenAll(uploadTasks);

        foreach (var image in images)
        {
            serviceEntity.Images?.Add(image);
        }

        return await _priceBookRepository.CreateService(serviceEntity, ctx);
    }

    public async Task<ServiceBundleEntity> AddServiceBundleAsync(UserContext userContext,
        AddServiceBundleRequest request, CancellationToken ctx)
    {
        var serviceBundleEntity = new ServiceBundleEntity(request, userContext);

        return await _priceBookRepository.CreateServiceBundle(serviceBundleEntity, ctx);
    }

    public async Task<MaterialEntity> AddMaterialAsync(UserContext userContext, AddMaterialRequest request,
        CancellationToken ctx)
    {
        var materialEntity = new MaterialEntity(request, userContext);

        if (request.Images == null || !request.Images.Any())
            return await _priceBookRepository.CreateMaterial(materialEntity, ctx);

        var uploadTasks = request.Images.Select(image => _imageRepository.UploadImageAsync(image, userContext.UserId, "materials", ctx))
            .ToList();
        var images = await Task.WhenAll(uploadTasks);

        foreach (var image in images)
        {
            materialEntity.Images?.Add(image);
        }

        return await _priceBookRepository.CreateMaterial(materialEntity, ctx);
    }

    public async Task<TaxRateEntity> AddTaxRateAsync(UserContext userContext, AddTaxRateRequest request,
        CancellationToken ctx)
    {
        var taxRateEntity = new TaxRateEntity(request, userContext);

        return await _priceBookRepository.CreateTaxRate(taxRateEntity, ctx);
    }

    public async Task<WarrantyEntity> AddWarrantyAsync(UserContext userContext, AddWarrantyRequest request,
        CancellationToken ctx)
    {
        var warrantyEntity = new WarrantyEntity(request, userContext);

        return await _priceBookRepository.CreateWarranty(warrantyEntity, ctx);
    }
}