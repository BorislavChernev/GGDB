namespace GoodGameDatabase.Web.ViewModels.Game
{
    public class EditGameViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string Description { get; set; }

        public string Status { get; set; }

        public bool SupportsWindows { get; set; }

        public bool SupportsLinux { get; set; }

        public bool SupportsMacOs { get; set; }
    }
}
