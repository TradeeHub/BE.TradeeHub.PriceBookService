using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Services;
using BE.TradeeHub.PriceBookService.Domain.Requests;
using BE.TradeeHub.PriceBookService.Domain.Responses;
using MongoDB.Bson;

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

    public async Task<ServiceCategoryEntity>
        AddNewServiceCategoryAsync(IUserContext userContext,
            AddNewServiceCategoryRequest request, CancellationToken ctx)
    {
        var newServiceCategory = new ServiceCategoryEntity(request, userContext);

        if (request.Images == null || !request.Images.Any())
            return await _priceBookRepository.CreateServiceCategoryAsync(newServiceCategory, ctx);

        var uploadTasks = request.Images
            .Select(image => _imageRepository.UploadImageAsync(image, userContext.UserId, "service-category", ctx))
            .ToList();
        var images = await Task.WhenAll(uploadTasks);

        foreach (var image in images)
        {
            newServiceCategory.Images?.Add(image);
        }

        return await _priceBookRepository.CreateServiceCategoryAsync(newServiceCategory, ctx);
    }

    public async Task<OperationResult> DeleteServiceCategoryAsync(IUserContext userContext, ObjectId Id, CancellationToken ctx)
    {
        var (operationResult, deletedServiceCategory)=  await _priceBookRepository.DeleteServiceCategoryAsync(userContext, Id, ctx);
        if (deletedServiceCategory == null) return operationResult;
        
        var keys = deletedServiceCategory.Images?.Select(i => i.S3Key).ToList();
        
        if(keys == null || keys.Count == 0)
            return operationResult;
        
        await _imageRepository.DeleteImagesAsync(keys, ctx, operationResult);
        
        return operationResult;
    }

    public async Task<LaborRateEntity> AddLaborRateAsync(IUserContext userContext, AddLaborRateRequest request,
        CancellationToken ctx)
    {
        var laborRateEntity = new LaborRateEntity(request, userContext);

        return await _priceBookRepository.CreateLabourRateAsync(laborRateEntity, ctx);
    }

    public async Task<ServiceEntity> AddServiceAsync(IUserContext userContext, AddServiceRequest request,
        CancellationToken ctx)
    {
        var serviceEntity = new ServiceEntity(request, userContext);

        if (request.Images == null || !request.Images.Any())
            return await _priceBookRepository.CreateServiceAsync(serviceEntity, ctx);

        var uploadTasks = request.Images
            .Select(image => _imageRepository.UploadImageAsync(image, userContext.UserId, "services", ctx))
            .ToList();
        var images = await Task.WhenAll(uploadTasks);

        foreach (var image in images)
        {
            serviceEntity.Images?.Add(image);
        }

        return await _priceBookRepository.CreateServiceAsync(serviceEntity, ctx);
    }

    public async Task<ServiceBundleEntity> AddServiceBundleAsync(IUserContext userContext,
        AddServiceBundleRequest request, CancellationToken ctx)
    {
        var serviceBundleEntity = new ServiceBundleEntity(request, userContext);

        return await _priceBookRepository.CreateServiceBundleAsync(serviceBundleEntity, ctx);
    }

    public async Task<MaterialEntity> AddMaterialAsync(IUserContext userContext, AddMaterialRequest request,
        CancellationToken ctx)
    {
        var materialEntity = new MaterialEntity(request, userContext);

        if (request.Images == null || !request.Images.Any())
            return await _priceBookRepository.CreateMaterialAsync(materialEntity, ctx);

        var uploadTasks = request.Images
            .Select(image => _imageRepository.UploadImageAsync(image, userContext.UserId, "materials", ctx))
            .ToList();
        var images = await Task.WhenAll(uploadTasks);

        foreach (var image in images)
        {
            materialEntity.Images?.Add(image);
        }

        return await _priceBookRepository.CreateMaterialAsync(materialEntity, ctx);
    }

    public async Task<TaxRateEntity> AddTaxRateAsync(IUserContext userContext, AddTaxRateRequest request,
        CancellationToken ctx)
    {
        var taxRateEntity = new TaxRateEntity(request, userContext);

        return await _priceBookRepository.CreateTaxRateAsync(taxRateEntity, ctx);
    }

    public async Task<WarrantyEntity> AddWarrantyAsync(IUserContext userContext, AddWarrantyRequest request,
        CancellationToken ctx)
    {
        var warrantyEntity = new WarrantyEntity(request, userContext);

        return await _priceBookRepository.CreateWarrantyAsync(warrantyEntity, ctx);
    }


    public async Task<IList<ServiceCategoryEntity>> GetAllServiceCategoriesAsync(IUserContext userContext, BsonDocument projection,
        CancellationToken ctx)
    {
        return await _priceBookRepository.GetAllServiceCategoriesAsync(userContext, projection, ctx);
    }
}