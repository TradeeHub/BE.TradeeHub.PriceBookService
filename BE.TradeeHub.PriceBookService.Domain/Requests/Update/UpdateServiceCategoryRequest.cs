using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests.Updates;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Requests.Update;

public class UpdateServiceCategoryRequest : IUpdateServiceCategoryRequest
{
    [ID]
    public ObjectId Id { get; set; }
    [ID]
    public ObjectId? ParentServiceCategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? S3KeyToDelete { get; set; }
    public IFile? NewImage { get; set; }
}