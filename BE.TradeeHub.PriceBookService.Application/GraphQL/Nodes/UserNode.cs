using BE.TradeeHub.PriceBookService.Domain.SubgraphEntities;

namespace BE.TradeeHub.PriceBookService.Application.GraphQL.Nodes;

[Node]
[ExtendObjectType(typeof(UserEntity))]
public static class UserNode;