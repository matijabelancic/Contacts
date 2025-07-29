namespace API.Utilities;

public class RequestParameters
{
    private const int MaxPageSize = 50;
    public int PageNumber { get; set; } = 1;

    private int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    private string? _searchTerm;
    public string? SearchTerm
    {
        get => _searchTerm;
        set => _searchTerm = value?.Trim().ToLower();
    }

    public string? SortBy { get; set; } = "Name";
    public bool SortByAscending { get; set; } = true;
}