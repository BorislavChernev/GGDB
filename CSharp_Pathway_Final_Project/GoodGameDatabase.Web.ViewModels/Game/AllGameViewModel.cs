using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoodGameDatabase.Web.Models.Game
{
    public class AllGameViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string ReleaseDate { get; set; }

        public string AgeRestriction { get; set; }

        public string ImageUrl { get; set; } = null!;
    }
}
