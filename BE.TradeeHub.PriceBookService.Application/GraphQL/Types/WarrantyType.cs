using BE.TradeeHub.PriceBookService.Domain.Entities;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Types;

public class WarrantyType: ObjectType<WarrantyEntity>
{
    protected override void Configure(IObjectTypeDescriptor<WarrantyEntity> descriptor)
    {
        descriptor.Field(c => c.Id).ID();

        descriptor.Ignore(x=>x.UserOwnerId);
        descriptor.Ignore(x=>x.CreatedBy);
        descriptor.Ignore(x=>x.ModifiedBy);
    }
}