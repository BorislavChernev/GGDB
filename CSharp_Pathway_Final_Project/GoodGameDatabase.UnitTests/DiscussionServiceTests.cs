using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GoodGameDatabase.Data;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Services.Data;
using GoodGameDatabase.Data.Model;

namespace GoodGameDatabase.UnitTests
{
    [TestFixture]
    public class DiscussionServiceTests
    {
        private ApplicationDbContext dbContext;
        private ILogger<IDiscussionService> logger;
        private IDiscussionService discussionService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            this.dbContext = new ApplicationDbContext(options);

            this.logger = new LoggerFactory().CreateLogger<IDiscussionService>();
            this.discussionService = new DiscussionService(this.logger, this.dbContext);
        }

        [Test]
        public async Task TestCreateNewDiscussionAsync()
        {
            // Arrange
            var newDiscussion = new Discussion
            {
                Topic = "Test Discussion",
                Description = "This is a test discussion.",
                DatePosted = DateTime.UtcNow,
                pinned = false,
                CreatorId = Guid.NewGuid()
            };

            // Act
            var createdDiscussionId = await this.discussionService.CreateNewAsync(newDiscussion);

            // Assert
            var createdDiscussion = await dbContext.Discussions.FindAsync(createdDiscussionId);
            Assert.NotNull(createdDiscussion);
            Assert.AreEqual(newDiscussion.Topic, createdDiscussion.Topic);
        }

        [Test]
        public async Task TestDeleteDiscussionByIdAsync()
        {
            // Arrange
            var discussionToDelete = new Discussion
            {
                Id = 5,
                Topic = "Test Discussion",
                Description = "This is a test discussion.",
                DatePosted = DateTime.UtcNow,
                pinned = false,
                CreatorId = Guid.NewGuid()
            };
            dbContext.Discussions.Add(discussionToDelete);
            await dbContext.SaveChangesAsync();

            // Act
            await this.discussionService.DeleteDiscussionByIdAsync(discussionToDelete.Id);

            // Assert
            var deletedDiscussion = await dbContext.Discussions.FindAsync(discussionToDelete.Id);
            Assert.Null(deletedDiscussion);
        }

        [Test]
        public async Task TestGetAllDiscussionsAsync()
        {
            // Arrange
            var discussions = new[]
            {
                new Discussion
                {
                    Id = 10,
                    Topic = "Test Discussion 1",
                    Description = "This is a test discussion 1.",
                    DatePosted = DateTime.UtcNow,
                    pinned = false,
                    CreatorId = Guid.NewGuid()
                },
                new Discussion
                {
                    Id = 2,
                    Topic = "Test Discussion 2",
                    Description = "This is a test discussion 2.",
                    DatePosted = DateTime.UtcNow,
                    pinned = true,
                    CreatorId = Guid.NewGuid()
                },
            };
            dbContext.Discussions.AddRange(discussions);
            await dbContext.SaveChangesAsync();

            // Act
            var allDiscussions = await this.discussionService.GetAllAsync();

            // Assert
            Assert.AreEqual(allDiscussions.Count, allDiscussions.Count);
        }

        [Test]
        public async Task TestGetBestThreeDiscussionsAsync()
        {
            // Arrange
            var discussions = new[]
            {
                new Discussion
                {
                    Id = 7,
                    Topic = "Test Discussion 1",
                    Description = "This is a test discussion 1.",
                    DatePosted = DateTime.UtcNow,
                    pinned = false,
                    CreatorId = Guid.NewGuid()
                },
                new Discussion
                {
                    Id = 8,
                    Topic = "Test Discussion 2",
                    Description = "This is a test discussion 2.",
                    DatePosted = DateTime.UtcNow,
                    pinned = true,
                    CreatorId = Guid.NewGuid()
                },
            };
            dbContext.Discussions.AddRange(discussions);
            await dbContext.SaveChangesAsync();

            // Act
            var bestThreeDiscussions = await this.discussionService.GetBestThreeDiscussionsAsync();

            // Assert
            Assert.AreEqual(3, bestThreeDiscussions.Count);
        }
    }
}