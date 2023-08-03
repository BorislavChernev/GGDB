using GoodGameDatabase.Data.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodGameDatabase.Data.Model
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
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

        [ForeignKey(nameof(Game))]
        [Required]
        public int GameId { get; set; }

        [Required]
        public Game Game { get; set; } = null!;
    }
}