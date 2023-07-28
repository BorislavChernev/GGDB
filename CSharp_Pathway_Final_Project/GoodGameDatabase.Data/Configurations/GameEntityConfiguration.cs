namespace HouseRentingSystem.Data.Configurations
{
    using GoodGameDatabase.Data.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static GoodGameDatabase.Data.Model.Enums.AgeRestrictionType;
    using static GoodGameDatabase.Data.Model.Enums.ReleaseStatusType;
    public class GameEntityConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasData(GenerateCategories());
        }

        private Game[] GenerateCategories()
        {
            ICollection<Game> games = new HashSet<Game>();

            Game game;

            game = new Game()
            {
                Id = 1,
                Name = "The Forest",
                Description = "As the lone survivor of a passenger jet crash, you find yourself in a mysterious forest battling to stay alive",
                Version = "v1.12.0 - VR",
                ReleaseDate = new DateTime(2014, 5, 30),
                Rating = 89,
                AgeRestriction = PEGI18,
                Status = OfficialRelease,
                SupportsWindows = true,
                SupportsLinux = false,
                SupportsMacOs = false,
                ImageUrl = "https://cdn.discordapp.com/attachments/856238725162729525/1134206890872688700/the-forest-banner.jpg",
                Reviews = new HashSet<Review>(),
                CreatorId = 1,
            };
            games.Add(game);

            game = new Game()
            {
                Id = 2,
                Name = "Need for Speed™ Heat",
                Description = "A thrilling race experience pits you against a city’s rogue police force.",
                Version = "v1.0.0",
                ReleaseDate = new DateTime(2019, 9, 8),
                Rating = 76,
                AgeRestriction = PEGI16,
                Status = OfficialRelease,
                SupportsWindows = true,
                SupportsLinux = false,
                SupportsMacOs = false,
                ImageUrl = "https://cdn.discordapp.com/attachments/856238725162729525/1134208308429991996/6552b32dea82b16326bbc34931dae2f4.png",
                Reviews = new HashSet<Review>(),
                CreatorId = 2,
            };
            games.Add(game);

            game = new Game()
            {
                Id = 3,
                Name = "Astroneer",
                Description = "Explore and reshape distant worlds!",
                Version = "v1.2.6",
                ReleaseDate = new DateTime(2016, 12, 16),
                Rating = 92,
                AgeRestriction = PEGI7,
                Status = OfficialRelease,
                SupportsWindows = true,
                SupportsLinux = false,
                SupportsMacOs = false,
                ImageUrl = "https://cdn.discordapp.com/attachments/856238725162729525/1134209239955538071/capsule_467x181.jpg",
                Reviews = new HashSet<Review>(),
                CreatorId = 3,
            };
            games.Add(game);

            return games.ToArray();
        }
    }
}