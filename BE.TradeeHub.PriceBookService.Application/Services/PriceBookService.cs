using Amazon.S3;
using BE.TradeeHub.PriceBookService.Application.Interfaces;
using BE.TradeeHub.PriceBookService.Application.Requests;
using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using Amazon.S3.Model;
using BE.TradeeHub.PriceBookService.Application.Mappings;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;

namespace BE.TradeeHub.PriceBookService.Application.Services;

public class PriceBookService : IPriceBookService
{
    private readonly IAmazonS3 _s3Client;
    private readonly IAppSettings _appSettings;
    private readonly IPriceBookRepository _priceBookRepository;

    public PriceBookService(IAmazonS3 s3Client, IAppSettings appSettings, IPriceBookRepository priceBookRepository)
    {
        _s3Client = s3Client;
        _appSettings = appSettings;
        _priceBookRepository = priceBookRepository;
    }

    public async Task<ServiceCategoryEntity> AddNewServiceCategoryAsync(UserContext userContext,
        AddNewServiceCategoryRequest request, CancellationToken ctx)
    {
        var newServiceCategory = request.ToServiceCategoryEntity(userContext.UserId, userContext.UserId);

        if (request.Images == null || !request.Images.Any())
            return await _priceBookRepository.CreateServiceCategory(newServiceCategory, ctx);

        var uploadTasks = request.Images.Select(image => UploadImageAsync(image, userContext.UserId, ctx)).ToList();
        var s3ImageKeys = await Task.WhenAll(uploadTasks);

        foreach (var s3ImageKey in s3ImageKeys)
        {
            newServiceCategory.ImagesS3Keys?.Add(s3ImageKey);
            newServiceCategory.Images?.Add($"{_appSettings.CloudFrontUrl}{s3ImageKey}");
        }

        return await _priceBookRepository.CreateServiceCategory(newServiceCategory, ctx);
    }

    public async Task<LaborRateEntity> AddLaborRateAsync(UserContext userContext, AddLaborRateRequest request,
        CancellationToken ctx)
    {
        var laborRateEntity = request.ToLaborRateEntity(userContext.UserId, userContext.UserId);
        
        return await _priceBookRepository.CreateLabourRate(laborRateEntity, ctx);
    }

    public async Task<ServiceEntity> AddServiceAsync(UserContext userContext, AddServiceRequest request,
        CancellationToken ctx)
    {
        var serviceEntity = request.ToServiceEntity(userContext.UserId, userContext.UserId);
        
        return await _priceBookRepository.CreateService(serviceEntity, ctx);
    }

    public async Task<ServiceBundleEntity> AddServiceBundleAsync(UserContext userContext,
        AddServiceBundleRequest request, CancellationToken ctx)
    {
        var serviceBundleEntity = request.ToServiceBundleEntity(userContext.UserId, userContext.UserId);
        
        return await _priceBookRepository.CreateServiceBundle(serviceBundleEntity, ctx);
    }

    public async Task<MaterialEntity> AddMaterialAsync(UserContext userContext, AddMaterialRequest request,
        CancellationToken ctx)
    {
        var materialEntity = request.ToMaterialEntity(userContext.UserId, userContext.UserId);
        
        return await _priceBookRepository.CreateMaterial(materialEntity, ctx);
    }

    public async Task<TaxRateEntity> AddTaxRateAsync(UserContext userContext, AddTaxRateRequest request,
        CancellationToken ctx)
    {
        var taxRateEntity = request.ToTaxRateEntity(userContext.UserId, userContext.UserId);
        
        return await _priceBookRepository.CreateTaxRate(taxRateEntity, ctx);
    }

    public async Task<WarrantyEntity> AddWarrantyAsync(UserContext userContext, AddWarrantyRequest request,
        CancellationToken ctx)
    {
        var warrantyEntity = request.ToWarrantyEntity(userContext.UserId, userContext.UserId);
        
        return await _priceBookRepository.CreateWarranty(warrantyEntity, ctx);
    }

    private async Task<string> UploadImageAsync(IFile image, Guid userId, CancellationToken cancellationToken)
    {
        var key = $"price-book/{userId}/service-category/{Guid.NewGuid()}_{image.Name}";

        await using var fileStream = image.OpenReadStream();

        var putRequest = new PutObjectRequest
        {
            BucketName = _appSettings.S3BucketName,
            Key = key,
            InputStream = fileStream, // Provide the memory stream with the file's content
            AutoCloseStream = true, // It's okay to auto-close now since we are using a using block
        };

        try
        {
            await _s3Client.PutObjectAsync(putRequest, cancellationToken);
            return key;
        }
        catch (Exception e)
        {
            var message = $"Failed to upload image to S3. {e.Message}";
            throw new Exception(message);
        }
    }
}