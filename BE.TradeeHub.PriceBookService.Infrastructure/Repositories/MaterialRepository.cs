using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;
using BE.TradeeHub.PriceBookService.Domain.Requests.Update;
using BE.TradeeHub.PriceBookService.Domain.Responses;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.PriceBookService.Infrastructure.Repositories;

public class MaterialRepository(IMongoDbContext dbContext) : IMaterialRepository
{
    public async Task<MaterialEntity> CreateMaterialAsync(MaterialEntity material, CancellationToken cancellationToken)
    {
        await dbContext.Materials.InsertOneAsync(material, cancellationToken: cancellationToken);
        return material;
    }
    
    public async Task<(OperationResult, MaterialEntity?)> DeleteMaterialAsync(ObjectId id, CancellationToken ctx)
    {
        var operationResult = new OperationResult();
        
        var deletedDocument = await dbContext.Materials.FindOneAndDeleteAsync(
            m => m.Id == id,
            null,
            ctx
        );

        return deletedDocument != null ? (operationResult, deletedDocument) : (operationResult.AddError("Material not found or already deleted."), null);
    }
    
    public async Task<MaterialEntity?> UpdateMaterialAsync(IUserContext userContext, UpdateMaterialRequest request,
        OperationResult operationResult, CancellationToken ctx, ImageEntity? newImage = null)
    {
        try
        {
            var updates = new List<UpdateDefinition<MaterialEntity>>();
            var updateBuilder = Builders<MaterialEntity>.Update;

            // Building update definitions for MaterialEntity
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

            if (!string.IsNullOrEmpty(request.Identifier))
            {
                updates.Add(updateBuilder.Set(e => e.Identifier, request.Identifier));
            }

            if (request.UsePriceRange.HasValue)
            {
                updates.Add(updateBuilder.Set(e => e.UsePriceRange, request.UsePriceRange.Value));
            }

            if (request.Taxable.HasValue)
            {
                updates.Add(updateBuilder.Set(e => e.Taxable, request.Taxable.Value));
            }

            if (request.AllowOnlineBooking.HasValue)
            {
                updates.Add(updateBuilder.Set(e => e.AllowOnlineBooking, request.AllowOnlineBooking.Value));
            }

            if (request.OnlinePrice.HasValue)
            {
                updates.Add(updateBuilder.Set(e => e.OnlinePrice, request.OnlinePrice.Value));
            }

            if (request.Cost.HasValue)
            {
                updates.Add(updateBuilder.Set(e => e.Cost, request.Cost.Value));
            }

            if (request.Price.HasValue)
            {
                updates.Add(updateBuilder.Set(e => e.Price, request.Price.Value));
            }

            if (!string.IsNullOrEmpty(request.UnitType))
            {
                updates.Add(updateBuilder.Set(e => e.UnitType, request.UnitType));
            }

            if (!string.IsNullOrEmpty(request.Vendor))
            {
                updates.Add(updateBuilder.Set(e => e.Vendor, request.Vendor));
            }

            if (request.PricingTiers != null && request.PricingTiers.Any())
            {
                updates.Add(updateBuilder.Set(e => e.PricingTiers, request.PricingTiers));
            }

            updates.Add(updateBuilder.Set(e => e.ModifiedById, userContext.UserId));
            updates.Add(updateBuilder.Set(e => e.ModifiedAt, DateTime.UtcNow));

            if (newImage != null)
            {
                updates.Add(updateBuilder.Push(e => e.Images, newImage));
            }

            // Perform the update
            var combinedUpdates = updateBuilder.Combine(updates);
            var updateResult =
                await dbContext.Materials.UpdateOneAsync(e => e.Id == request.Id, combinedUpdates, null, ctx);

            if (updateResult.IsAcknowledged && updateResult.ModifiedCount > 0)
            {
                operationResult.AddMessage("Material updated successfully");
                // Retrieve and return the updated entity
                var updatedEntity = await dbContext.Materials.Find(e => e.Id == request.Id).FirstOrDefaultAsync(ctx);
                return updatedEntity;
            }

            operationResult.AddError("Failed to update material or no changes were made.");
            return null;
        }
        catch (Exception e)
        {
            operationResult.AddError(e.Message);
            return null;
        }
    }
}