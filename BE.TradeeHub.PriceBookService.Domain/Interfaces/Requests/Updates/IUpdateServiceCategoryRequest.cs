using HotChocolate.Types;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests.Updates;

[InterfaceType]
public interface IUpdateServiceCategoryRequest
{ 
    public ObjectId Id { get; set; }
    public ObjectId? ParentServiceCategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? S3KeyToDelete { get; set; }
    public IFile? NewImage { get; set; }
}