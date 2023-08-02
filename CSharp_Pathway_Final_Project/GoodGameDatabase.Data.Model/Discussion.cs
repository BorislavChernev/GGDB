using System.ComponentModel.DataAnnotations;

namespace GoodGameDatabase.Data.Model
{
    public class Discussion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Topic { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime DatePosted { get; set; }

        [Required]
        public bool pinned { get; set; }

        public IdentityUserDiscussion IdentityUserDiscussions { get; set; }
    }
}
