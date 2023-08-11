namespace GoodGameDatabase.Web.ViewModels.Message
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public int DiscussionId { get; set; }
        public string SenderId { get; set; }
        public string SenderUsername { get; set; }
        public string Content { get; set; }
        public string Timestamp { get; set; }
    }
}
