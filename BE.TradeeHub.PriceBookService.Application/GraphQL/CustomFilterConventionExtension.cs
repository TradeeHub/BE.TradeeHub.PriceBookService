using HotChocolate.Data;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL;

public class CustomFilterConventionExtension : FilterConventionExtension
{
    protected override void Configure(IFilterConventionDescriptor descriptor)
    {
        base.Configure(descriptor);
        
        descriptor.BindRuntimeType<ObjectId, StringOperationFilterInputType>();
        //
        // descriptor.Provider(
        //     new QueryableFilterProvider(
        //         x => x
        //             .AddDefaultFieldHandlers()
        //             .AddFieldHandler<QueryableStringInvariantEqualsHandler>()));
    }
}