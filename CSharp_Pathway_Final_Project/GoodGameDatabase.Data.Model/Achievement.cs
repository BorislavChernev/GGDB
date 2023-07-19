using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodGameDatabase.Data.Model
{
    public class Achievement
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }

        [Required]
        public Game Game { get; set; } = null!;
    }
}
