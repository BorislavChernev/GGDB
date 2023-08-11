using GoodGameDatabase.Web.ViewModels.Message;

namespace GoodGameDatabase.Web.ViewModels.Discussion
{
    public class DiscussionDetailsViewModel
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string DateCreated { get; set; }
        public bool Pinned { get; set; }
        public string CreatorId { get; set; }
        public string CreatorUsername { get; set; }
        public int ParticipantCount { get; set; }
        public int MessageCount { get; set; }

        public ICollection<MessageViewModel> Messages { get; set; }
    }
}
