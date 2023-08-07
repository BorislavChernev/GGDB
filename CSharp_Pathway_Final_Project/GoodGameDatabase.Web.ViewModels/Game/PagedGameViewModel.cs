using GoodGameDatabase.Web.ViewModels.Game;

public class PagedGameViewModel
{
    public ICollection<AllGameViewModel> Games { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalGames { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
}