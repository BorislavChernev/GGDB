namespace HouseRentingSystem.Data.Configurations
{
    using GoodGameDatabase.Data.Model;
    using Microsoft.EntityFrameworkCore;

    using static GoodGameDatabase.Data.Model.Enums.LanguageType;
    using static GoodGameDatabase.Data.Model.Enums.CategoryType;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class GuideEntityConfiguration : IEntityTypeConfiguration<Guide>
    {
        public void Configure(EntityTypeBuilder<Guide> builder)
        {
            builder.HasData(GenerateCategories());
        }

        private Guide[] GenerateCategories()
        {
            ICollection<Guide> guides = new HashSet<Guide>();

            Guide guide;

            guide = new Guide()
            {
                Id = 1,
                Title = "How to stay alive in The Forest",
                Rating = 95,
                Subtitle = "Forest living tips",
                Description = "Do not be afraid of canibals",
                Language = English,
                GameId = 1,
                Category = GameplayBasic,

            };
            guides.Add(guide);

            guide = new Guide()
            {
                Id = 2,
                Title = "How to complete Astroneer",
                Rating = 89,
                Subtitle = "Tips to complete Astroneer faster",
                Description = "Play by the rules",
                Language = English,
                GameId = 3,
                Category = Walkthroughs,

            };
            guides.Add(guide);

            guide = new Guide()
            {
                Id = 3,
                Title = "What car do you need to outrace the final boss in NSFW Heat",
                Rating = 100,
                Subtitle = "some that you can buy",
                Description = "You need to buy the last car to beat the boss",
                Language = English,
                GameId = 2,
                Category = Walkthroughs,

            };
            guides.Add(guide);

            return guides.ToArray();
        }
    }
}