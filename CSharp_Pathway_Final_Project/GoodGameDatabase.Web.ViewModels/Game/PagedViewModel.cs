using GoodGameDatabase.Web.ViewModels.Game;

public class PagedViewModel
{
    public string Action { get; set; }
    public string Controller { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalViewModels { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
}