namespace HouseRentingSystem.Data.Configurations
{
    using GoodGameDatabase.Data.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DiscussionEntityConfiguration : IEntityTypeConfiguration<Discussion>
    {
        public void Configure(EntityTypeBuilder<Discussion> builder)
        {
            builder.HasData(GenerateCategories());
        }

        private Discussion[] GenerateCategories()
        {
            ICollection<Discussion> discussions = new HashSet<Discussion>();

            Discussion discussion;

            discussion = new Discussion()
            {
                Id = 1,
                Topic = "Is Cheating in games bad",
                Description = "This discussion is about cheating in singleplayer games",
                DatePosted = new DateTime(2023, 4, 17),
                pinned = true,
                Reviews = new HashSet<Review>(),
            };
            discussions.Add(discussion);

            discussion = new Discussion()
            {
                Id = 2,
                Topic = "How to survive in The Forest",
                Description = "This discussion is about surviving in The Forest",
                DatePosted = new DateTime(2023, 5, 1),
                pinned = false,
                Reviews = new HashSet<Review>(),
            };
            discussions.Add(discussion);

            discussion = new Discussion()
            {
                Id = 3,
                Topic = "Astroneer dust storms",
                Description = "This discussion is about dust storms in Astroneer",
                DatePosted = new DateTime(2023, 3, 12),
                pinned = false,
                Reviews = new HashSet<Review>(),
            };
            discussions.Add(discussion);

            return discussions.ToArray();
        }
    }
}