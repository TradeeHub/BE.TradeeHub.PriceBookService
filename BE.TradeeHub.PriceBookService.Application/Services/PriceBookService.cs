using Amazon.S3;
using Amazon.S3.Transfer;
using BE.TradeeHub.PriceBookService.Application.Interfaces;
using BE.TradeeHub.PriceBookService.Application.Requests;
using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Path = System.IO.Path;

namespace BE.TradeeHub.PriceBookService.Application.Services;

public class PriceBookService : IPriceBookService
{
    private readonly IAmazonS3 _s3Client;
    private readonly IAppSettings _appSettings;
    private readonly IMongoCollection<ServiceCategoryEntity> _serviceCategoryCollection;

    public PriceBookService(IAmazonS3 s3Client, IAppSettings appSettings)
    {
        _s3Client = s3Client;
        _appSettings = appSettings;
        
        var client = new MongoClient(appSettings.MongoDbConnectionString);
        var database = client.GetDatabase(appSettings.MongoDbDatabaseName);
        _serviceCategoryCollection = database.GetCollection<ServiceCategoryEntity>("ServiceCategories");
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
            Images = new List<string>()
        };

        // if (request.Images != null && request.Images.Count > 0)
        // {
        //     foreach (var imageFile in request.Images)
        //     {
        //         await using var fileStream = imageFile.OpenReadStream();
        //         var fileExtension = Path.GetExtension(imageFile.FileName); // Extract the extension
        //
        //         var s3ImageKey = await UploadImageAsync(fileStream, userContext.UserId, fileExtension, ctx);
        //
        //         newServiceCategory.ImagesS3Keys?.Add(s3ImageKey);
        //         newServiceCategory.Images?.Add($"{_appSettings.CloudFrontUrl}{s3ImageKey}");
        //
        //     }
        // }

        await _serviceCategoryCollection.InsertOneAsync(newServiceCategory, null, ctx);
        return newServiceCategory;
    }

    private async Task<string> UploadImageAsync(Stream fileStream, Guid userId, string fileExtension, CancellationToken cancellationToken)
    {
        var key = $"price-book/{userId}/service-category/{Guid.NewGuid()}{fileExtension}"; // Append the extension to the key
        var putRequest = new PutObjectRequest
        {
            BucketName = _appSettings.S3BucketName,
            Key = key,
            InputStream = fileStream,
            AutoCloseStream = false, // Set to true if you want AWS SDK to close stream automatically
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
