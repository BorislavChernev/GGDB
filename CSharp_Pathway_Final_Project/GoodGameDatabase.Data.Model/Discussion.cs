using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GoodGameDatabase.Common.EntityValidationConstants.Discussion;

namespace GoodGameDatabase.Data.Model
{
    public class Discussion
    {
        public Discussion()
        {
            this.Participants = new HashSet<DiscussionParticipant>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(TopicMaxLength, MinimumLength = TopicMinLength, ErrorMessage = TopicErrorMessage)]
        public string Topic { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = DescriptionErrorMessage)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime DatePosted { get; set; }

        [Required]
        public bool pinned { get; set; }

        [Required]
        public Guid CreatorId { get; set; }

        [Required]
        [ForeignKey(nameof(CreatorId))]   
        public ApplicationUser Creator { get; set; }

        public ICollection<DiscussionParticipant> Participants { get; set; }
    }
}
