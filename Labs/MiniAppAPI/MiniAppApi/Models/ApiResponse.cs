namespace MiniAppApi.Models;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T? Data { get; set; }
    public DateTime Timestamp { get; set; }

    public ApiResponse(T? data, string message = "Operation successful")
    {
        Success = true;
        Data = data;
        Message = message;
        Timestamp = DateTime.UtcNow;
    }

    public ApiResponse(string message, bool success = false)
    {
        Success = success;
        Message = message;
        Data = default;
        Timestamp = DateTime.UtcNow;
    }
}

public class PaginatedResponse<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (TotalCount + PageSize - 1) / PageSize;
    public bool HasNextPage => PageNumber < TotalPages;
    public bool HasPreviousPage => PageNumber > 1;
}

