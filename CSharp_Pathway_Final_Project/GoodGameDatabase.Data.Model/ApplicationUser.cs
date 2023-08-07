using Microsoft.AspNetCore.Identity;

namespace GoodGameDatabase.Data.Model
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
            this.Discussions = new HashSet<DiscussionParticipant>();
        }
        public ICollection<DiscussionParticipant> Discussions { get; set; }
    }
}