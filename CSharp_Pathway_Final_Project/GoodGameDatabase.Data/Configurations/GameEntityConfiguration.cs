﻿namespace HouseRentingSystem.Data.Configurations
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
                Description = "As the lone survivor of a passenger jet crash, you find yourself in a mysterious forest battling to stay alive against a society of cannibalistic mutants.\r\n\r\nBuild, explore, survive in this terrifying first person survival horror simulator.",
                Version = "v1.12.0 - VR",
                ReleaseDate = new DateTime(2014, 5, 30),
                AgeRestriction = PEGI18,
                Status = OfficialRelease,
                SupportsWindows = true,
                SupportsLinux = false,
                SupportsMacOs = false,
                ImageUrl = "https://cdn.discordapp.com/attachments/856238725162729525/1134206890872688700/the-forest-banner.jpg",
                Reviews = new HashSet<Review>(),
                CreatorId = 1,
                Creator = new Creator() { }

            };
            games.Add(game);

            game = new Game()
            {
                Id = 2,
                Name = "Need for Speed™ Heat",
                Description = "A thrilling race experience pits you against a city’s rogue police force as you battle your way into street racing’s elite.\r\n\r\nHustle by day and risk it all at night in Need for Speed™ Heat, a white-knuckle street racer, where the lines of the law fade as the sun starts to set. By day, Palm City hosts the Speedhunter Showdown, a sanctioned competition where you earn Bank to customize and upgrade your high-performance cars. At night, ramp up the intensity in illicit street races that build your reputation, getting you access to bigger races and better parts. But stay ready – cops are waiting and not all of them play fair.",
                Version = "v1.0.0",
                ReleaseDate = new DateTime(2019, 9, 8),
                AgeRestriction = PEGI16,
                Status = OfficialRelease,
                SupportsWindows = true,
                SupportsLinux = false,
                SupportsMacOs = false,
                ImageUrl = "https://cdn.discordapp.com/attachments/856238725162729525/1134208308429991996/6552b32dea82b16326bbc34931dae2f4.png",
                Reviews = new HashSet<Review>(),
                CreatorId = 2,
                Creator = new Creator() { }

            };
            games.Add(game);

            game = new Game()
            {
                Id = 3,
                Name = "Astroneer",
                Description = "Explore and reshape distant worlds! Astroneer is set during the 25th century Intergalactic Age of Discovery, where Astroneers explore the frontiers of outer space, risking their lives in harsh environments to unearth rare discoveries and unlock the mysteries of the universe.\r\n\r\nIn this space sandbox adventure, players can work together to build custom bases above or below ground, create vehicles to explore a vast solar system, and use terrain to create anything they can imagine. A player’s creativity and ingenuity are the key to thriving on exciting planetary adventures! In Astroneer you can:",
                Version = "v1.2.6",
                ReleaseDate = new DateTime(2016, 12, 16),
                AgeRestriction = PEGI7,
                Status = OfficialRelease,
                SupportsWindows = true,
                SupportsLinux = false,
                SupportsMacOs = false,
                ImageUrl = "https://cdn.discordapp.com/attachments/856238725162729525/1134209239955538071/capsule_467x181.jpg",
                Reviews = new HashSet<Review>(),
                CreatorId = 3,
                Creator = new Creator() { }

            };
            games.Add(game);

            return games.ToArray();
        }
    }
}