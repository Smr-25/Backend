namespace MiniAppApi.Dtos;

public class PaginationParams
{
    private const int MaxPageSize = 100;
    private int _pageSize = 10;

    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }

    public string SortBy { get; set; } = "Id";
    public bool Ascending { get; set; } = true;
}

