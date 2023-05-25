
namespace LiveScoreLib.Application;

public class CustomResult
{
    private Exception? Exception { get; set; }
    
    private CustomResult(bool success)
    {
        IsSuccess = success;
    }

    public bool IsSuccess { get; set; }
    public bool IsFailed => !IsSuccess;

    private object? _value;

    public static CustomResult Success(object value) => new(true) { _value = value};
    public static CustomResult Fail(Exception exception) => new(false) { Exception = exception };

    public string? GetExceptionMessage() => Exception?.Message;
    
    public T GetValue<T>() => 
        IsSuccess ? (T)(_value ?? throw new Exception("The value is null")) : throw new Exception("No value because this is a failed result.");
}