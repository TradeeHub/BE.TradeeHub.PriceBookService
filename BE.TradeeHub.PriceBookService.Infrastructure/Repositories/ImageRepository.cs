using Amazon.S3;
using Amazon.S3.Model;
using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;
using BE.TradeeHub.PriceBookService.Domain.Responses;
using HotChocolate.Types;

namespace BE.TradeeHub.PriceBookService.Infrastructure.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly IAmazonS3 _s3Client;
    private readonly IAppSettings _appSettings;

    public ImageRepository(IAmazonS3 s3Client, IAppSettings appSettings)
    {
        _s3Client = s3Client;
        _appSettings = appSettings;
    }

    public async Task<ImageEntity> UploadImageAsync(IFile image, Guid userId, string folderName,
        CancellationToken cancellationToken)
    {
        var key = $"price-book/{userId}/{folderName}/{Guid.NewGuid()}_{image.Name}";

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
            var imageCloudFrontUrl = $"{_appSettings.CloudFrontUrl}{key}";

            return new ImageEntity(imageCloudFrontUrl, key, image.Name, image.Length, image.ContentType, userId);
        }
        catch (Exception e)
        {
            var message = $"Failed to upload image to S3. {e.Message}";
            throw new Exception(message);
        }
    }

    public async Task DeleteImagesAsync(List<string> imageKeys, CancellationToken cancellationToken,
        OperationResult? operationResult = null)
    {
        var deleteObjectsRequest = new DeleteObjectsRequest
        {
            BucketName = _appSettings.S3BucketName,
            Objects = imageKeys.ConvertAll(key => new KeyVersion { Key = key })
        };
        
        var response = await _s3Client.DeleteObjectsAsync(deleteObjectsRequest, cancellationToken);

        // Logging successful deletions
        foreach (var deletedObject in response.DeletedObjects)
        {
            operationResult?.AddMessage($"Successfully deleted: {deletedObject.Key}");
        }

        // Handling deletion errors
        if (response.DeleteErrors.Count > 0)
        {
            foreach (var error in response.DeleteErrors)
            {
                operationResult?.AddError($"Key: {error.Key}, Code: {error.Code}, Message: {error.Message}");
            }
        }
        else
        {
            operationResult?.AddMessage("All images deleted successfully.");
        }
    }
}