using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoodGameDatabase.Data.Model
{
    public class IdentityUserGuides
    {
        [ForeignKey(nameof(Guide))]
        public Guide? GuideId { get; set; }

        [Required]
        public Guide? Guide { get; set; } = null!;


        [ForeignKey(nameof(User))]
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; } = null!;
    }
}
