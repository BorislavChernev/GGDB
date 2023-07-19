using GoodGameDatabase.Data.Model;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(ApplicationDbContext)) ??
                Assembly.GetExecutingAssembly();

            builder.Entity<Creator>()
                .HasMany(g => g.DevelopedGames);

            base.OnModelCreating(builder);
        }
    }
}