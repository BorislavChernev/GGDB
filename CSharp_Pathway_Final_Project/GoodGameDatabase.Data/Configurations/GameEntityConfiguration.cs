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
                AgeRestriction = PEGI18,
                Status = OfficialRelease,
                SupportsPC = true,
                SupportsPS = true,
                SupportsXbox = true,
                SupportsNintendo = false,
                ImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/242760/capsule_616x353.jpg?t=1666811027",
                Reviews = new HashSet<Review>(),
            };
            games.Add(game);

            game = new Game()
            {
                Id = 2,
                Name = "Need for Speed™ Heat",
                Description = "A thrilling race experience pits you against a city’s rogue police force.",
                Version = "v1.0.0",
                ReleaseDate = new DateTime(2019, 9, 8),
                AgeRestriction = PEGI16,
                Status = OfficialRelease,
                SupportsPC = true,
                SupportsPS = false,
                SupportsXbox = false,
                SupportsNintendo = false,
                ImageUrl = "https://cdn.cloudflare.steamstatic.com/steam/apps/1222680/capsule_616x353.jpg?t=1690398297",
                Reviews = new HashSet<Review>(),
            };
            games.Add(game);

            game = new Game()
            {
                Id = 3,
                Name = "Astroneer",
                Description = "Explore and reshape distant worlds!",
                Version = "v1.2.6",
                ReleaseDate = new DateTime(2016, 12, 16),
                AgeRestriction = PEGI7,
                Status = OfficialRelease,
                SupportsPC = true,
                SupportsPS = false,
                SupportsXbox = false,
                SupportsNintendo = false,
                ImageUrl = "https://cdn.akamai.steamstatic.com/steam/apps/361420/capsule_616x353.jpg?t=1689355883",
                Reviews = new HashSet<Review>(),
            };
            games.Add(game);

            return games.ToArray();
        }
    }
}