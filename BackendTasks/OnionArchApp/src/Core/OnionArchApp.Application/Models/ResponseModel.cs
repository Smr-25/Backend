namespace OnionArchApp.Application.Models;

public class ResponseModel<T>
{
    public T? Data { get; set; }
    public bool IsSuccess { get; set; }
    public List<string>? Errors { get; set; }

    public static ResponseModel<T> Success(T data)
    {
        return new ResponseModel<T>
        {
            Data = data,
            IsSuccess = true,
            Errors = null
        };
    }

    public static ResponseModel<T> Failure(string error)
    {
        return new ResponseModel<T>
        {
            Data = default,
            IsSuccess = false,
            Errors = [error]
        };
    }

    public static ResponseModel<T> Failure(List<string> errors)
    {
        return new ResponseModel<T>
        {
            Data = default,
            IsSuccess = false,
            Errors = errors
        };
    }
}