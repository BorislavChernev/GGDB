using System.ComponentModel.DataAnnotations;

namespace GoodGameDatabase.Data.Model
{
    public class Wish
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        public int GameId { get; set; }

        [Required]
        public Game Game { get; set; } = null!;
    }
}