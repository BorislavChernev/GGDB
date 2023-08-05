﻿using GoodGameDatabase.Data.Model.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GoodGameDatabase.Common.EntityValidationConstants.Game;

namespace GoodGameDatabase.Data.Model
{
    public class Game
    {
        public Game()
        {
            this.Reviews = new HashSet<Review>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = NameErrorMessage)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = DescriptionErrorMessage)]
        public string Description { get; set; } = null!;

        public string? Version { get; set; } = null!;

        public DateTime? ReleaseDate { get; set; }

        public int Rating { get; set; }

        [Required]
        public AgeRestrictionType AgeRestriction { get; set; }

        [Required]
        public ReleaseStatusType Status { get; set; }

        [Required]
        public bool SupportsPC { get; set; }

        [Required]
        public bool SupportsPS { get; set; }

        [Required]
        public bool SupportsXbox { get; set; }
        
        [Required]
        public bool SupportsNintendo { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!;

        public ICollection<Review> Reviews { get; set; }
        
        //Developer ---------->
        [Required]
        [ForeignKey(nameof(Creator))]
        public int CreatorId { get; set; }

        [Required]
        public Creator Creator { get; set; }
        //Developer ---------->
    }
}
