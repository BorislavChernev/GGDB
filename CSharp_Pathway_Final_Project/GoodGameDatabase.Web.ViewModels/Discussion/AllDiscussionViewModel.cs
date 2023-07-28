namespace GoodGameDatabase.Web.ViewModels.Discussion
{
    public class AllDiscussionViewModel
    {
        public int Id { get; set; }

        public string Topic { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string DatePosted { get; set; }

        public bool pinned { get; set; }

        public int ReviewsCount { get; set; }
    }
}
