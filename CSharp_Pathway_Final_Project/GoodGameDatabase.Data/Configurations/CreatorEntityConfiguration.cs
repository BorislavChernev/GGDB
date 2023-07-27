namespace HouseRentingSystem.Data.Configurations
{
    using GoodGameDatabase.Data.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using static GoodGameDatabase.Data.Model.Enums.AgeRestrictionType;
    using static GoodGameDatabase.Data.Model.Enums.ReleaseStatusType;
    public class CreatorEntityConfiguration : IEntityTypeConfiguration<Creator>
    {
        public void Configure(EntityTypeBuilder<Creator> builder)
        {
            builder.HasData(GenerateCategories());
        }

        private Creator[] GenerateCategories()
        {
            ICollection<Creator> creators = new HashSet<Creator>();

            Creator creator;

            creator = new Creator()
            {
                Id = 1,
                Name = "Endnight Games Ltd",
                DateEstablished = new DateTime(2013, 1, 1),
                DevelopedGames = new HashSet<Game>(),

            };
            creators.Add(creator);
            
            creator = new Creator()
            {
                Id = 2,
                Name = "Ghost Games",
                DateEstablished = new DateTime(2011, 1, 1),
                DevelopedGames = new HashSet<Game>(),

            };
            creators.Add(creator);
            
            creator = new Creator()
            {
                Id = 3,
                Name = "System Era Softworks",
                DateEstablished = new DateTime(2014, 1, 1),
                DevelopedGames = new HashSet<Game>(),

            };
            creators.Add(creator);

            return creators.ToArray();
        }
    }
}