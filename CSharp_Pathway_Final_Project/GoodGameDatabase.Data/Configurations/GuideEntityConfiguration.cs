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
                Language = English,
                GameId = 2,
                Category = Walkthroughs,

            };
            guides.Add(guide);

            return guides.ToArray();
        }
    }
}