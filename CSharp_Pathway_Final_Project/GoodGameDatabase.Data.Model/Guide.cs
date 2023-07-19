using GoodGameDatabase.Data.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodGameDatabase.Data.Model
{
    public class Guide
    {
        public Guide()
        {
            this.Participants = new HashSet<ApplicationUser>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public double? Rating { get; set; }

        //Creator ---------->
        [ForeignKey(nameof(Author))]
        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        public ApplicationUser Author { get; set; } = null!;
        //Creator ---------->

        [Required]
        public LanguageType Language { get; set; }

        [Required]
        public CategoryType Category { get; set; }

        public ICollection<ApplicationUser> Participants { get; set; }
    }
}
