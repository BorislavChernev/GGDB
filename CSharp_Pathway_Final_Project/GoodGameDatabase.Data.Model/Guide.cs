using GoodGameDatabase.Data.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace GoodGameDatabase.Data.Model
{
    public class Guide
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public double? Rating { get; set; }

        [Required]
        public LanguageType Language { get; set; }

        [Required]
        public CategoryType Category { get; set; }

        [Required]
        public IdentityUserGuide IdentityUserGuide { get; set; } = null!;
    }
}
