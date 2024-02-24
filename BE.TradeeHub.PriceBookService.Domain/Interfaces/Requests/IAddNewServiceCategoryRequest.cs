using HotChocolate.Types;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;

public interface IAddNewServiceCategoryRequest
{
    public ObjectId? ParentServiceCategoryId { get; }
    public string Name { get; }
    public string? Description { get; }
    public IEnumerable<IFile>? Images { get; }
}