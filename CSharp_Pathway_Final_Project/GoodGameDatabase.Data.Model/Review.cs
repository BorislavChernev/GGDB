using GoodGameDatabase.Data.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GoodGameDatabase.Common.EntityValidationConstants.Review;

namespace GoodGameDatabase.Data.Model
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = DescriptionErrorMessage)]
        public string Description { get; set; } = null!;

        [Required]
        public ReviewType Type { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        [ForeignKey(nameof(AuthorId))]
        public ApplicationUser Author { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }

        [Required]
        public Game Game { get; set; } = null!;
    }
}