using System.ComponentModel.DataAnnotations;

namespace GoodGameDatabase.Web.ViewModels.Guide
{
    public class AllGuideViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public double? Rating { get; set; }

        public string Language { get; set; }

        public string Category { get; set; }
    }
}
