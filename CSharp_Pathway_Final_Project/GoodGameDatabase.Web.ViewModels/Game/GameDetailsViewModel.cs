namespace GoodGameDatabase.Web.ViewModels.Game
{
    public class GameDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? ReleaseDate { get; set; }

        public string ImageUrl { get; set; } = null!;

        public int Rating { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }

        public bool SupportsWindows { get; set; }

        public bool SupportsLinux { get; set; }

        public bool SupportsMacOs { get; set; }
    }
}
