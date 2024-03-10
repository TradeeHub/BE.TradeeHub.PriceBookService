using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;
using BE.TradeeHub.PriceBookService.Domain.Requests.Update;
using BE.TradeeHub.PriceBookService.Domain.Responses;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Infrastructure.Repositories;

public class PriceBookRepository(IMongoDbContext dbContext) : IPriceBookRepository
{
    public async Task<ServiceCategoryEntity> CreateServiceCategoryAsync(ServiceCategoryEntity serviceCategory,
        CancellationToken cancellationToken)
    {
        await dbContext.ServiceCategories.InsertOneAsync(serviceCategory, cancellationToken: cancellationToken);

        if (!serviceCategory.ParentServiceCategoryId.HasValue) return serviceCategory;

        var update = Builders<ServiceCategoryEntity>.Update.AddToSet(sce => sce.ServiceCategoryIds, serviceCategory.Id);

        await dbContext.ServiceCategories.UpdateOneAsync(
            sce => sce.Id == serviceCategory.ParentServiceCategoryId.Value,
            update,
            cancellationToken: cancellationToken
        );

        return serviceCategory;
    }

    public async Task<ServiceCategoryEntity?> UpdateServiceCategoryAsync(IUserContext userContext,
        UpdateServiceCategoryRequest request, OperationResult operationResult, CancellationToken ctx,
        ImageEntity? newImage = null)
    {
        using var session = await dbContext.Client.StartSessionAsync(cancellationToken: ctx);
        session.StartTransaction();
        try
        {
            var updates = new List<UpdateDefinition<ServiceCategoryEntity>>();
            var updateBuilder = Builders<ServiceCategoryEntity>.Update;

            // Explicitly checking conditions and building update definitions
            if (!string.IsNullOrEmpty(request.Name))
            {
                updates.Add(updateBuilder.Set(e => e.Name, request.Name));
            }

            if (request.Description != null)
            {
                updates.Add(updateBuilder.Set(e => e.Description, request.Description));
            }

            if (request.ParentServiceCategoryId.HasValue)
            {
                updates.Add(updateBuilder.Set(e => e.ParentServiceCategoryId, request.ParentServiceCategoryId));
            }

            updates.Add(updateBuilder.Set(e => e.ModifiedById, userContext.UserId));
            
            updates.Add(updateBuilder.Set(e => e.ModifiedAt, DateTime.UtcNow));

            // Perform updates to non-array fields
            if (updates.Count > 0)
            {
                var combinedFieldUpdates = updateBuilder.Combine(updates);
                await dbContext.ServiceCategories.UpdateOneAsync(session, e => e.Id == request.Id, combinedFieldUpdates, new UpdateOptions { IsUpsert = false }, ctx);
            }

            // Handling images separately
            if (!string.IsNullOrEmpty(request.S3KeyToDelete))
            {
                var pullUpdate = updateBuilder.PullFilter(e => e.Images, img => img.S3Key == request.S3KeyToDelete);
                await dbContext.ServiceCategories.UpdateOneAsync(session, e => e.Id == request.Id, pullUpdate, new UpdateOptions { IsUpsert = false }, ctx);
            }

            if (newImage != null)
            {
                var pushUpdate = updateBuilder.Push(e => e.Images, newImage);
                await dbContext.ServiceCategories.UpdateOneAsync(session, e => e.Id == request.Id, pushUpdate, new UpdateOptions { IsUpsert = false }, ctx);
            }
            
            await session.CommitTransactionAsync(ctx);

            // Retrieve and return the updated entity
            var updatedEntity = await dbContext.ServiceCategories.Find(session, e => e.Id == request.Id)
                .FirstOrDefaultAsync(ctx);
            if (updatedEntity != null)
            {
                operationResult.AddMessage("Service category updated successfully");
                return updatedEntity;
            }

            operationResult.AddError("Failed to update service category");
            return null;
        }
        catch (Exception e)
        {
            await session.AbortTransactionAsync(ctx);
            operationResult.AddError(e.Message);
            return null;
        }
    }

    public async Task<(OperationResult, ServiceCategoryEntity?)> DeleteServiceCategoryAsync(IUserContext userContext,
        ObjectId id, CancellationToken ctx)
    {
        var operationResult = new OperationResult();
        var serviceCategory = await dbContext.ServiceCategories.Find(sc => sc.Id == id).FirstOrDefaultAsync(ctx);

        if (serviceCategory == null)
        {
            return (operationResult.AddError($"Service category with ID {id} not found."), serviceCategory);
        }

        var unlinkTasks = new List<Task>
        {
            UnlinkEntitiesFromServiceCategoryAsync<ServiceCategoryEntity>(id, serviceCategory.Name,
                "service categories", ctx, operationResult),
            UnlinkEntitiesFromServiceCategoryAsync<LaborRateEntity>(id, serviceCategory.Name, "labor rates", ctx,
                operationResult),
            UnlinkEntitiesFromServiceCategoryAsync<MaterialEntity>(id, serviceCategory.Name, "materials", ctx,
                operationResult),
            UnlinkEntitiesFromServiceCategoryAsync<ServiceEntity>(id, serviceCategory.Name, "services", ctx,
                operationResult),
            UnlinkEntitiesFromServiceCategoryAsync<WarrantyEntity>(id, serviceCategory.Name, "warranties", ctx,
                operationResult),
            UnlinkEntitiesFromServiceCategoryAsync<ServiceBundleEntity>(id, serviceCategory.Name, "service bundles",
                ctx, operationResult)
        };

        await Task.WhenAll(unlinkTasks);

        if (operationResult.Errors?.Count > 0)
        {
            return (operationResult, serviceCategory);
        }

        var deleteResult = await dbContext.ServiceCategories.DeleteOneAsync(sc => sc.Id == id, ctx);

        return deleteResult.IsAcknowledged switch
        {
            true when deleteResult.DeletedCount > 0 => (operationResult, serviceCategory),
            _ => (operationResult.AddError("Failed to delete the service category."), serviceCategory)
        };
    }

    public async Task<LaborRateEntity> CreateLabourRateAsync(LaborRateEntity laborRate,
        CancellationToken cancellationToken)
    {
        await dbContext.LabourRates.InsertOneAsync(laborRate, cancellationToken: cancellationToken);
        return laborRate;
    }

    public async Task<ServiceEntity> CreateServiceAsync(ServiceEntity service, CancellationToken cancellationToken)
    {
        await dbContext.Services.InsertOneAsync(service, cancellationToken: cancellationToken);
        return service;
    }

    public async Task<ServiceBundleEntity> CreateServiceBundleAsync(ServiceBundleEntity serviceBundle,
        CancellationToken cancellationToken)
    {
        await dbContext.ServiceBundles.InsertOneAsync(serviceBundle, cancellationToken: cancellationToken);
        return serviceBundle;
    }

    public async Task<MaterialEntity> CreateMaterialAsync(MaterialEntity material, CancellationToken cancellationToken)
    {
        await dbContext.Materials.InsertOneAsync(material, cancellationToken: cancellationToken);
        return material;
    }

    public async Task<TaxRateEntity> CreateTaxRateAsync(TaxRateEntity taxRate, CancellationToken cancellationToken)
    {
        await dbContext.TaxRates.InsertOneAsync(taxRate, cancellationToken: cancellationToken);
        return taxRate;
    }

    public async Task<WarrantyEntity> CreateWarrantyAsync(WarrantyEntity warranty, CancellationToken ctx)
    {
        await dbContext.Warranties.InsertOneAsync(warranty, cancellationToken: ctx);
        return warranty;
    }

    public async Task<IList<ServiceCategoryEntity>> GetAllServiceCategoriesAsync(
        IUserContext userContext, BsonDocument projection, CancellationToken ctx)
    {
        // Apply the projection in the MongoDB query
        var filter = Builders<ServiceCategoryEntity>.Filter.Eq(x => x.UserOwnerId, userContext.UserId);
        var sort = Builders<ServiceCategoryEntity>.Sort.Descending(x => x.ModifiedAt).Descending(x => x.CreatedAt);

        return await dbContext.ServiceCategories
            .Find(filter)
            .Sort(sort)
            .Project<ServiceCategoryEntity>(projection)
            .ToListAsync(ctx);
    }

    private async Task UnlinkLaborRatesFromServiceCategoryAsync(ObjectId parentServiceCategoryId,
        string serviceCategoryName, CancellationToken ctx, OperationResult? operationResult = null)
    {
        var filter = Builders<LaborRateEntity>.Filter.Eq(lr => lr.ParentServiceCategoryId, parentServiceCategoryId);
        var update = Builders<LaborRateEntity>.Update.Set(lr => lr.ParentServiceCategoryId, null);
        var result = await dbContext.LabourRates.UpdateManyAsync(filter, update, cancellationToken: ctx);

        if (result.IsAcknowledged && result.ModifiedCount > 0)
        {
            operationResult?.AddMessage(
                $"{result.ModifiedCount} labor rates unlinked from service category {serviceCategoryName}.");
        }

        {
            operationResult?.AddMessage(
                $"{result.ModifiedCount} labor rates unlinked from service category {serviceCategoryName}.");
        }
    }

    private async Task UnlinkServiceCategoriesFromServiceCategoryAsync(ObjectId parentServiceCategoryId,
        string serviceCategoryName, CancellationToken ctx, OperationResult? operationResult = null)
    {
        var filter =
            Builders<ServiceCategoryEntity>.Filter.Eq(lr => lr.ParentServiceCategoryId, parentServiceCategoryId);
        var update = Builders<ServiceCategoryEntity>.Update.Set(lr => lr.ParentServiceCategoryId, null);
        var result = await dbContext.ServiceCategories.UpdateManyAsync(filter, update, cancellationToken: ctx);

        if (result.IsAcknowledged && result.ModifiedCount > 0)
        {
            operationResult?.AddMessage(
                $"{result.ModifiedCount} service categories unlinked from service category {serviceCategoryName}.");
        }

        {
            operationResult?.AddMessage(
                $"{result.ModifiedCount} service categories unlinked from service category {serviceCategoryName}.");
        }
    }

    private async Task UnlinkEntitiesFromServiceCategoryAsync<T>(ObjectId parentServiceCategoryId,
        string serviceCategoryName, string entityMessage, CancellationToken ctx,
        OperationResult? operationResult = null) where T : class
    {
        // Attempt to find a collection property within dbContext of type IMongoCollection<T>
        var collectionProperty = typeof(IMongoDbContext).GetProperties()
            .FirstOrDefault(p => p.PropertyType == typeof(IMongoCollection<T>));
        if (collectionProperty == null)
        {
            operationResult?.AddError($"No collection found for type {typeof(T).Name}.");
            return;
        }

        var collection = (IMongoCollection<T>)collectionProperty.GetValue(dbContext)!;

        // Check if the type T has a field named 'ParentServiceCategoryId' for direct unlinking
        if (typeof(T).GetProperty("ParentServiceCategoryId") != null)
        {
            var filter = Builders<T>.Filter.Eq("ParentServiceCategoryId", parentServiceCategoryId);
            var update = Builders<T>.Update.Set("ParentServiceCategoryId", BsonNull.Value);
            await UpdateAndLogAsync(collection, filter, update, entityMessage, serviceCategoryName, operationResult,
                ctx);
        }

        // Check if the type T has a list field named 'ServiceCategoryIds' for array element removal
        if (typeof(T).GetProperty("ServiceCategoryIds") != null)
        {
            var filter = Builders<T>.Filter.AnyEq("ServiceCategoryIds", parentServiceCategoryId);
            var update = Builders<T>.Update.Pull("ServiceCategoryIds", parentServiceCategoryId);
            await UpdateAndLogAsync(collection, filter, update, entityMessage, serviceCategoryName, operationResult,
                ctx);
        }
    }

    private async Task UpdateAndLogAsync<T>(IMongoCollection<T> collection, FilterDefinition<T> filter,
        UpdateDefinition<T> update, string entityMessage, string serviceCategoryName, OperationResult? operationResult,
        CancellationToken ctx) where T : class
    {
        var result = await collection.UpdateManyAsync(filter, update, cancellationToken: ctx);
        if (result.IsAcknowledged)
        {
            if (result.ModifiedCount > 0)
            {
                var message =
                    $"{result.ModifiedCount} {entityMessage} unlinked from service category {serviceCategoryName}.";
                operationResult?.AddMessage(message);
            }
        }
        else
        {
            operationResult?.AddError("Operation not acknowledged by the database.");
        }
    }
}