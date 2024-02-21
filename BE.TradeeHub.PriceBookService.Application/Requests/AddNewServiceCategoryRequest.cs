using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Application.Requests;

public class AddNewServiceCategoryRequest
{
    public ObjectId? ParentServiceCategory { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    // public IFile? Images { get; set; }
}