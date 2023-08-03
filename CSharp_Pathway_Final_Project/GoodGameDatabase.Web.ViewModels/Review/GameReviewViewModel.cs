namespace GoodGameDatabase.Web.ViewModels.Review
{
    public class GameReviewViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public string Author { get; set; }
    }
}
