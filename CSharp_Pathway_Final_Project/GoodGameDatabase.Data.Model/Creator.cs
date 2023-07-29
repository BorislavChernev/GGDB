using System.ComponentModel.DataAnnotations;

namespace GoodGameDatabase.Data.Model
{
    public class Creator
    {
        public Creator()
        {
            this.DevelopedGames = new HashSet<Game>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime DateEstablished { get; set; }

        [Required]
        public ICollection<Game> DevelopedGames { get; set; }
    }
}
