using HotChocolate.Data.Filters;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL;

public class CustomFilterConventionExtension : FilterConventionExtension
{
    protected override void Configure(IFilterConventionDescriptor descriptor)
    {
        base.Configure(descriptor);
        
        descriptor.BindRuntimeType<ObjectId, StringOperationFilterInputType>();
    }
}