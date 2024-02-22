using Amazon.S3;
using BE.TradeeHub.PriceBookService.Application.Interfaces;
using BE.TradeeHub.PriceBookService.Application.Requests;
using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using Amazon.S3.Model;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;
using Path = System.IO.Path;

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
        var newServiceCategory = new ServiceCategoryEntity
        {
            Name = request.Name,
            UserOwnerId = userContext.UserId,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = userContext.UserId,
            Images = new List<string>(),
            ImagesS3Keys = new List<string>()
        };

        if (request.Images == null) return await _priceBookRepository.CreateServiceCategory(newServiceCategory, ctx);
        
        foreach (var imageFile in request.Images)
        {
            var s3ImageKey = await UploadImageAsync(imageFile, userContext.UserId, ctx);

            newServiceCategory.ImagesS3Keys
                .Add(s3ImageKey); // ImagesS3Keys list is now guaranteed to be initialized
            newServiceCategory.Images.Add(
                $"{_appSettings.CloudFrontUrl}{s3ImageKey}"); // Ensure the URL is constructed correctly
        }

        return await _priceBookRepository.CreateServiceCategory(newServiceCategory, ctx);
    }

    private async Task<string> UploadImageAsync(IFile image, Guid userId, CancellationToken cancellationToken)
    {
        // Generate the key with the file extension
        var fileExtension = Path.GetExtension(image.Name);
        var key = $"price-book/{userId}/service-category/{Guid.NewGuid()}{image.Name}{fileExtension}";

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
            var response = await _s3Client.PutObjectAsync(putRequest, cancellationToken);
            return key;
        }
        catch (Exception e)
        {
            var message = $"Failed to upload image to S3. {e.Message}";
            throw new Exception(message);
        }
    }
}