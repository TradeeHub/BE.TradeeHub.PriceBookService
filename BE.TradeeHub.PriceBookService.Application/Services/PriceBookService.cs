﻿using BE.TradeeHub.PriceBookService.Domain.Entities;
using BE.TradeeHub.PriceBookService.Domain.Interfaces;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;
using BE.TradeeHub.PriceBookService.Domain.Interfaces.Services;
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

    public async Task<ServiceCategoryEntity>
        AddNewServiceCategoryAsync(IUserContext userContext,
            AddNewServiceCategoryRequest request, CancellationToken ctx)
    {
        var newServiceCategory = new ServiceCategoryEntity(request, userContext);

        if (request.Images == null || !request.Images.Any())
            return await _priceBookRepository.CreateServiceCategory(newServiceCategory, ctx);

        var uploadTasks = request.Images
            .Select(image => _imageRepository.UploadImageAsync(image, userContext.UserId, "service-category", ctx))
            .ToList();
        var images = await Task.WhenAll(uploadTasks);

        foreach (var image in images)
        {
            newServiceCategory.Images?.Add(image);
        }

        return await _priceBookRepository.CreateServiceCategory(newServiceCategory, ctx);
    }

    public async Task<LaborRateEntity> AddLaborRateAsync(IUserContext userContext, AddLaborRateRequest request,
        CancellationToken ctx)
    {
        var laborRateEntity = new LaborRateEntity(request, userContext);

        return await _priceBookRepository.CreateLabourRate(laborRateEntity, ctx);
    }

    public async Task<ServiceEntity> AddServiceAsync(IUserContext userContext, AddServiceRequest request,
        CancellationToken ctx)
    {
        var serviceEntity = new ServiceEntity(request, userContext);

        if (request.Images == null || !request.Images.Any())
            return await _priceBookRepository.CreateService(serviceEntity, ctx);

        var uploadTasks = request.Images
            .Select(image => _imageRepository.UploadImageAsync(image, userContext.UserId, "services", ctx))
            .ToList();
        var images = await Task.WhenAll(uploadTasks);

        foreach (var image in images)
        {
            serviceEntity.Images?.Add(image);
        }

        return await _priceBookRepository.CreateService(serviceEntity, ctx);
    }

    public async Task<ServiceBundleEntity> AddServiceBundleAsync(IUserContext userContext,
        AddServiceBundleRequest request, CancellationToken ctx)
    {
        var serviceBundleEntity = new ServiceBundleEntity(request, userContext);

        return await _priceBookRepository.CreateServiceBundle(serviceBundleEntity, ctx);
    }

    public async Task<MaterialEntity> AddMaterialAsync(IUserContext userContext, AddMaterialRequest request,
        CancellationToken ctx)
    {
        var materialEntity = new MaterialEntity(request, userContext);

        if (request.Images == null || !request.Images.Any())
            return await _priceBookRepository.CreateMaterial(materialEntity, ctx);

        var uploadTasks = request.Images
            .Select(image => _imageRepository.UploadImageAsync(image, userContext.UserId, "materials", ctx))
            .ToList();
        var images = await Task.WhenAll(uploadTasks);

        foreach (var image in images)
        {
            materialEntity.Images?.Add(image);
        }

        return await _priceBookRepository.CreateMaterial(materialEntity, ctx);
    }

    public async Task<TaxRateEntity> AddTaxRateAsync(IUserContext userContext, AddTaxRateRequest request,
        CancellationToken ctx)
    {
        var taxRateEntity = new TaxRateEntity(request, userContext);

        return await _priceBookRepository.CreateTaxRate(taxRateEntity, ctx);
    }

    public async Task<WarrantyEntity> AddWarrantyAsync(IUserContext userContext, AddWarrantyRequest request,
        CancellationToken ctx)
    {
        var warrantyEntity = new WarrantyEntity(request, userContext);

        return await _priceBookRepository.CreateWarranty(warrantyEntity, ctx);
    }
}