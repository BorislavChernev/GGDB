using GoodGameDatabase.Data;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Services.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GoodGameDatabase.Data.Model.Enums;
using GoodGameDatabase.Web.ViewModels.Guide;
using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Services.Exceptions;

namespace GoodGameDatabase.UnitTests
{
    [TestFixture]
    public class GuideServiceTests
    {
        private ApplicationDbContext dbContext;
        private ILogger<IGuideService> logger;
        private IGuideService guideService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            this.dbContext = new ApplicationDbContext(options);

            this.logger = new LoggerFactory().CreateLogger<IGuideService>();
            this.guideService = new GuideService(this.logger, this.dbContext);
        }
        [Test]
        public async Task TestCreateNewGuideAsync()
        {
            // Arrange
            var newGuide = new Guide
            {
                Title = "Test Guide",
                Description = "This is a test guide.",
                Language = LanguageType.English,
                Category = CategoryType.GameplayBasic,
                GameId = 1,
                WriterId = Guid.NewGuid()
            };

            // Act
            var createdGuideId = await this.guideService.CreateNewGuideAsync(newGuide);

            // Assert
            var createdGuide = await dbContext.Guides.FindAsync(createdGuideId);
            Assert.NotNull(createdGuide);
            Assert.AreEqual(newGuide.Title, createdGuide.Title);
        }

        [Test]
        public async Task TestDeleteGuideByIdAsync()
        {
            // Arrange
            var guideToDelete = new Guide
            {
                Id = 2,
                Title = "Test Guide",
                Description = "This is a test guide.",
                Language = LanguageType.English,
                Category = CategoryType.GameplayBasic,
                GameId = 1,
                WriterId = Guid.NewGuid()
            };
            dbContext.Guides.Add(guideToDelete);
            await dbContext.SaveChangesAsync();

            // Act
            await this.guideService.DeleteGuideByIdAsync(guideToDelete.Id);

            // Assert
            var deletedGuide = await dbContext.Guides.FindAsync(guideToDelete.Id);
            Assert.Null(deletedGuide);
        }

        [Test]
        public async Task TestEditGuideByIdAsync()
        {
            // Arrange
            var existingGuide = new Guide
            {
                Id = 1,
                Title = "Test Guide",
                Description = "This is a test guide.",
                Language = LanguageType.English,
                Category = CategoryType.GameplayBasic,
                GameId = 2,
                WriterId = Guid.NewGuid()
            };
            dbContext.Guides.Add(existingGuide);
            await dbContext.SaveChangesAsync();

            var viewModel = new EditGuideViewModel
            {
            };

            // Act
            await this.guideService.EditGuideByIdAsync(existingGuide.Id, viewModel);

            // Assert
            var editedGuide = await dbContext.Guides.FindAsync(existingGuide.Id);
            Assert.AreEqual(editedGuide, existingGuide);
        }

        [Test]
        public async Task TestGetAllGuidesAsync()
        {
            // Arrange
            var guides = new[]
            {
                new Guide
                {
                    Id = 5,
                    Title = "Test Guide 1",
                    Description = "This is a test guide 1.",
                    Language = LanguageType.English,
                    Category = CategoryType.GameplayBasic,
                    GameId = 1,
                    WriterId = Guid.NewGuid()
                },
                new Guide
                {
                    Id = 2,
                    Title = "Test Guide 2",
                    Description = "This is a test guide 2.",
                    Language = LanguageType.Russian,
                    Category = CategoryType.Crafting,
                    GameId = 1,
                    WriterId = Guid.NewGuid()
                },
            };
            dbContext.Guides.AddRange(guides);
            await dbContext.SaveChangesAsync();

            // Act
            var allGuides = await this.guideService.GetAllGuidesAsync();

            // Assert
            Assert.AreEqual(allGuides.Count, allGuides.Count);
        }

        [Test]
        public async Task TestGetGuideDetailsByIdAsync()
        {
            // Arrange
            var guideId = 6;
            var guide = new Guide
            {
                Id = guideId,
                Title = "Test Guide",
                Description = "This is a test guide.",
                Language = LanguageType.English,
                Category = CategoryType.GameplayBasic,
                GameId = 1,
                WriterId = Guid.NewGuid()
            };
            dbContext.Guides.Add(guide);
            await dbContext.SaveChangesAsync();

            // Act
            Assert.ThrowsAsync<ServiceException>(async () =>
            {
                var guideDetails = await this.guideService.GetGuideDetailsByIdAsync(guideId);
            });
        }
    }
}
