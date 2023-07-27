using GoodGameDatabase.Data.Model;
using HouseRentingSystem.Data.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GoodGameDatabase.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Creator> Creators { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<New> News { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<IdentityUserGame> IdentityUserGames { get; set; }
        public DbSet<IdentityUserGuide> IdentityUserGuides { get; set; }
        public DbSet<IdentityUserDiscussion> IdentityUserDiscussions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(ApplicationDbContext)) ??
                Assembly.GetExecutingAssembly();

            builder.Entity<Creator>()
                .HasMany(c => c.DevelopedGames);

            builder.Entity<IdentityUserGame>()
                .HasKey(ug => new { ug.UserId, ug.GameId });

            builder.Entity<IdentityUserGuide>()
                .HasKey(ug => new { ug.UserId, ug.GuideId });

            builder.Entity<IdentityUserDiscussion>()
                .HasKey(ug => new { ug.UserId, ug.DiscussionId });

            builder.ApplyConfiguration(new GameEntityConfiguration());
            builder.ApplyConfiguration(new CreatorEntityConfiguration());
            base.OnModelCreating(builder);
            DbContextOptionsBuilder opts = new DbContextOptionsBuilder();
            opts.EnableSensitiveDataLogging();
        }
    }
}