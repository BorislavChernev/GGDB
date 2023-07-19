﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodGameDatabase.Data.Model
{
    public class Discussion
    {
        public Discussion()
        {
            this.Reviews = new HashSet<Review>();
            this.Participants = new HashSet<ApplicationUser>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Topic { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public DateTime DatePosted { get; set; }

        [Required]
        public bool pinned { get; set; }

        //Creator ---------->
        [ForeignKey(nameof(Creator))]
        [Required]
        public int CreatorId { get; set; }

        [Required]
        public Creator Creator { get; set; } = null!;
        //Creator ---------->

        public ICollection<Review> Reviews { get; set; }

        public ICollection<ApplicationUser> Participants { get; set; }
    }
}
