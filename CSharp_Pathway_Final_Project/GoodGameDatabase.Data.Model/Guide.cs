using GoodGameDatabase.Data.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodGameDatabase.Data.Model
{
    public class Guide
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public LanguageType Language { get; set; }

        [Required]
        public CategoryType Category { get; set; }

        [Required]
        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }

        [Required]
        public Game Game { get; set; } = null!;

        [Required]
        public Guid WriterId { get; set; }

        [Required]
        [ForeignKey(nameof(WriterId))]
        public ApplicationUser Writer { get; set; } = null!;
    }
}
