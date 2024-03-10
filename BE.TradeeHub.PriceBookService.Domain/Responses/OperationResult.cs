using BE.TradeeHub.PriceBookService.Domain.Interfaces;

namespace BE.TradeeHub.PriceBookService.Domain.Responses;

public class OperationResult : IOperationResult
{
    public bool Success => Errors == null || Errors.Count == 0;
    public List<string>? Messages { get; protected set; }
    public List<string>? Errors { get; private set; }

    public OperationResult()
    {
        Errors = new List<string>();
    }

    
    public OperationResult AddMessage(string error)
    {
        Messages ??= [];
        if (!Messages.Contains(error))
        {
            Messages.Add(error);
        }

        return this;
    }
    
    public OperationResult AddError(string error)
    {
        Errors ??= [];
        if (!Errors.Contains(error))
        {
            Errors.Add(error);
        }

        return this;
    }
    
    public static OperationResult SuccessResult(string message)
    {
        return new OperationResult
        {
            Messages = new List<string> { message }
        };
    }

    public static OperationResult FailureResult(List<string> errors)
    {
        return new OperationResult
        {
            Errors = errors
        };
    }

    public static OperationResult FailureResult(string error)
    {
        return new OperationResult
        {
            Errors = new List<string> { error }
        };
    }
}

public class OperationResult<T> : OperationResult
{
    public T? Data { get; private set; } // Encapsulation: Data should be privately settable.

    public OperationResult() : base() { } // Keep the default constructor private.

    // Adjusting the constructor for setting data and message.
    public OperationResult(T data, string message = "") : base()
    {
        Data = data;
        if (!string.IsNullOrEmpty(message))
        {
            AddMessage(message); // Utilize the AddMessage method from the base class.
        }
    }
    
    public void AddData(T data)
    {
        Data = data;
    }

    // Adjusting the factory method for creating a success result.
    public static OperationResult<T> SuccessResult(T? data = default, string message = "")
    {
        var result = new OperationResult<T>(data);
        if (!string.IsNullOrEmpty(message))
        {
            result.AddMessage(message);
        }
        return result;
    }
}