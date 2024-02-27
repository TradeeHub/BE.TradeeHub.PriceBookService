using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Requests;

public class AddNewServiceCategoryRequest : IAddNewServiceCategoryRequest
{
    [ID]
    public ObjectId? ParentServiceCategoryId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public IEnumerable<IFile>? Images { get; set; }
}