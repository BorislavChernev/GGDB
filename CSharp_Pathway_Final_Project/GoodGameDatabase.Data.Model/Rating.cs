using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodGameDatabase.Data.Model
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }

        [Required]
        public Game Game { get; set; } = null!;
    }
}