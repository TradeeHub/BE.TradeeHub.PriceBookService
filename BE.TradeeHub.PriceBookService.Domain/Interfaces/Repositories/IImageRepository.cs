using BE.TradeeHub.PriceBookService.Domain.Entities;
using HotChocolate.Types;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Repositories;

public interface IImageRepository
{
    Task<ImageEntity> UploadImageAsync(IFile image, Guid userId, string folderName, CancellationToken cancellationToken);
}