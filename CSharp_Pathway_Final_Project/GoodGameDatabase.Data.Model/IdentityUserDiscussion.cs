using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoodGameDatabase.Data.Model
{
    public class IdentityUserDiscussion
    {
        [ForeignKey(nameof(Discussion))]
        public int DiscussionId { get; set; }

        [Required]
        public Discussion Discussion { get; set; } = null!;


        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; } = null!;
    }
}
