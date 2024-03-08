using HotChocolate.Types;

namespace BE.TradeeHub.PriceBookService.Domain.Interfaces;

[InterfaceType]
public interface IOperationResult
{
    public bool Success { get; }
    public List<string>? Messages { get; }
    public List<string>? Errors { get; }
}