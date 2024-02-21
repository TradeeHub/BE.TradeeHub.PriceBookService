using BE.TradeeHub.PriceBookService.Domain.Entities;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Types;

public class MaterialType : ObjectType<MaterialEntity>
{
    protected override void Configure(IObjectTypeDescriptor<MaterialEntity> descriptor)
    {
        descriptor.Field(c => c.Id).ID();

        descriptor.Ignore(x => x.UserOwnerId);
        descriptor.Ignore(x => x.CreatedBy);
        descriptor.Ignore(x => x.ModifiedBy);
    }
}