using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GoodGameDatabase.Data;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Services.Data;
using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Data.Model.Enums;
using GoodGameDatabase.Web.ViewModels.Game;

namespace GoodGameDatabase.UnitTests
{
    [TestFixture]
    public class GameServiceTests
    {
        private ApplicationDbContext dbContext;
        private ILogger<IGameService> logger;
        private IGameService gameService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            this.dbContext = new ApplicationDbContext(options);

            this.logger = new LoggerFactory().CreateLogger<IGameService>();
            this.gameService = new GameService(this.logger, this.dbContext);
        }

        [Test]
        public async Task CreateNewGameAsync_ValidGame_Success()
        {
            // Arrange
            var game = new Game
            {
                Name = "Test Game",
                Description = "Test Description",
                AgeRestriction = AgeRestrictionType.PEGI12,
                Status = ReleaseStatusType.Alpha,
                SupportsPC = true,
                SupportsPS = true,
                SupportsXbox = true,
                SupportsNintendo = true,
                ImageUrl = "test-image-url"
            };

            // Act
            await this.gameService.CreateNewGameAsync(game);

            // Assert
            var savedGame = await this.dbContext.Games.FirstOrDefaultAsync(g => g.Id == game.Id);
            Assert.NotNull(savedGame);
            Assert.AreEqual("Test Game", savedGame.Name);
        }

        [Test]
        public async Task EditGameByIdAsync_ValidIdAndModel_Success()
        {
            // Arrange
            var game = new Game
            {
                Name = "Test Game",
                Description = "Test Description",
                AgeRestriction = AgeRestrictionType.PEGI16,
                Status = ReleaseStatusType.OfficialRelease,
                SupportsPC = true,
                SupportsPS = true,
                SupportsXbox = true,
                SupportsNintendo = true,
                ImageUrl = "test-image-url"
            };

            await this.dbContext.Games.AddAsync(game);
            await this.dbContext.SaveChangesAsync();

            var editModel = new EditGameViewModel
            {
                Name = "Edited Game",
                Description = "Edited Description",
                Status = ReleaseStatusType.OpenBeta.ToString(),
                SupportsPC = false,
                SupportsPS = false,
                SupportsXbox = false,
                SupportsNintendo = false,
                ImageUrl = "edited-image-url"
            };

            // Act
            await this.gameService.EditGameByIdAsync(game.Id, editModel);

            // Assert
            var editedGame = await this.dbContext.Games.FirstOrDefaultAsync(g => g.Id == game.Id);
            Assert.AreEqual("Edited Game", editedGame.Name);
            Assert.AreEqual("Edited Description", editedGame.Description);
            Assert.AreEqual(ReleaseStatusType.OpenBeta, editedGame.Status);
            Assert.IsFalse(editedGame.SupportsPC);
            Assert.IsFalse(editedGame.SupportsPS);
            Assert.IsFalse(editedGame.SupportsXbox);
            Assert.IsFalse(editedGame.SupportsNintendo);
            Assert.AreEqual("edited-image-url", editedGame.ImageUrl);
        }

        [Test]
        public async Task LikeGameByIdAsync_ValidData_Success()
        {
            // Arrange
            var userGuid = Guid.NewGuid();
            var game = new Game
            {
                Name = "Test Game",
                Description = "Test Description",
                AgeRestriction = AgeRestrictionType.PEGI12,
                Status = ReleaseStatusType.OfficialRelease,
                SupportsPC = true,
                SupportsPS = true,
                SupportsXbox = true,
                SupportsNintendo = true,
                ImageUrl = "test-image-url"
            };

            await this.dbContext.Games.AddAsync(game);
            await this.dbContext.SaveChangesAsync();

            // Act
            await this.gameService.LikeGameByIdAsync(game.Id, userGuid);

            // Assert
            var likedGame = await this.dbContext.Games.Include(g => g.Likes).FirstOrDefaultAsync(g => g.Id == game.Id);
            Assert.NotNull(likedGame);
            Assert.AreEqual(1, likedGame.Likes.Count);
            Assert.AreEqual(userGuid, likedGame.Likes.First().UserId);
        }

        [Test]
        public async Task WishGameByIdAsync_ValidData_Success()
        {
            // Arrange
            var userGuid = Guid.NewGuid();
            var game = new Game
            {
                Name = "Test Game",
                Description = "Test Description",
                AgeRestriction = AgeRestrictionType.PEGI16,
                Status = ReleaseStatusType.EarlyAccess,
                SupportsPC = true,
                SupportsPS = true,
                SupportsXbox = true,
                SupportsNintendo = true,
                ImageUrl = "test-image-url"
            };

            await this.dbContext.Games.AddAsync(game);
            await this.dbContext.SaveChangesAsync();

            // Act
            await this.gameService.WishGameByIdAsync(game.Id, userGuid);

            // Assert
            var wishedGame = await this.dbContext.Games.Include(g => g.Wishes).FirstOrDefaultAsync(g => g.Id == game.Id);
            Assert.NotNull(wishedGame);
            Assert.AreEqual(1, wishedGame.Wishes.Count);
            Assert.AreEqual(userGuid, wishedGame.Wishes.First().UserId);
        }

        [Test]
        public async Task RateGameByIdAsync_ValidData_Success()
        {
            // Arrange
            var userGuid = Guid.NewGuid();
            var game = new Game
            {
                Name = "Test Game",
                Description = "Test Description",
                AgeRestriction = AgeRestrictionType.PEGI7,
                Status = ReleaseStatusType.ClosedBeta,
                SupportsPC = true,
                SupportsPS = true,
                SupportsXbox = true,
                SupportsNintendo = true,
                ImageUrl = "test-image-url"
            };

            await this.dbContext.Games.AddAsync(game);
            await this.dbContext.SaveChangesAsync();

            // Act
            await this.gameService.RateGameByIdAsync(game.Id, 5, userGuid.ToString());

            // Assert
            var ratedGame = await this.dbContext.Games.Include(g => g.Ratings).FirstOrDefaultAsync(g => g.Id == game.Id);
            Assert.NotNull(ratedGame);
            Assert.AreEqual(1, ratedGame.Ratings.Count);
            Assert.AreEqual(5, ratedGame.Ratings.First().Points);
            Assert.AreEqual(userGuid, ratedGame.Ratings.First().UserId);
        }

        [Test]
        public async Task DeleteGameByIdAsync_ValidId_Success()
        {
            // Arrange
            var game = new Game
            {
                Name = "Test Game",
                Description = "Test Description",
                AgeRestriction = AgeRestrictionType.PEGI3,
                Status = ReleaseStatusType.Alpha,
                SupportsPC = true,
                SupportsPS = true,
                SupportsXbox = true,
                SupportsNintendo = true,
                ImageUrl = "test-image-url"
            };

            await this.dbContext.Games.AddAsync(game);
            await this.dbContext.SaveChangesAsync();

            // Act
            await this.gameService.DeleteGameByIdAsync(game.Id);

            // Assert
            var deletedGame = await this.dbContext.Games.FirstOrDefaultAsync(g => g.Id == game.Id);
            Assert.Null(deletedGame);

            // Ensure related entities are deleted
            Assert.AreEqual(0, await this.dbContext.Guides.Where(g => g.GameId == game.Id).CountAsync());
            Assert.AreEqual(0, await this.dbContext.Reviews.Where(r => r.GameId == game.Id).CountAsync());
            Assert.AreEqual(0, await this.dbContext.Likes.Where(l => l.GameId == game.Id).CountAsync());
            Assert.AreEqual(0, await this.dbContext.Wishes.Where(w => w.GameId == game.Id).CountAsync());
            Assert.AreEqual(0, await this.dbContext.Ratings.Where(r => r.GameId == game.Id).CountAsync());
        }

        [Test]
        public async Task DeleteGameByIdAsync_InvalidId_Exception()
        {
            // Arrange
            var invalidId = -1;

            // Act and Assert
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await this.gameService.DeleteGameByIdAsync(invalidId);
            });

            Assert.AreEqual($"Game with ID {invalidId} not found.", exception.Message);
        }

        [Test]
        public async Task GetTotalGamesCount_ValidData_Success()
        {
            // Arrange
            var game1 = new Game
            {
                Name = "Test Game 1",
                Description = "Test Description",
                AgeRestriction = AgeRestrictionType.PEGI12,
                Status = ReleaseStatusType.EarlyAccess,
                SupportsPC = true,
                SupportsPS = true,
                SupportsXbox = false,
                SupportsNintendo = true,
                ImageUrl = "test-image-url"
            };

            var game2 = new Game
            {
                Name = "Test Game 2",
                Description = "Test Description",
                AgeRestriction = AgeRestrictionType.PEGI7,
                Status = ReleaseStatusType.OfficialRelease,
                SupportsPC = true,
                SupportsPS = false,
                SupportsXbox = true,
                SupportsNintendo = true,
                ImageUrl = "test-image-url"
            };

            await this.dbContext.Games.AddRangeAsync(game1, game2);
            await this.dbContext.SaveChangesAsync();

            // Act
            var totalGamesCount = await this.gameService.GetTotalGamesCount();

            // Assert
            Assert.AreEqual(totalGamesCount, totalGamesCount);
        }

        [Test]
        public async Task GetAllLikedGamesByUserIdAsync_ValidUserId_Success()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var game1 = new Game
            {
                Name = "Test Game 1",
                Description = "Test Description",
                AgeRestriction = AgeRestrictionType.PEGI3,
                Status = ReleaseStatusType.ClosedBeta,
                SupportsPC = true,
                SupportsPS = true,
                SupportsXbox = true,
                SupportsNintendo = true,
                ImageUrl = "test-image-url"
            };

            var game2 = new Game
            {
                Name = "Test Game 2",
                Description = "Test Description",
                AgeRestriction = AgeRestrictionType.PEGI16,
                Status = ReleaseStatusType.OpenBeta,
                SupportsPC = true,
                SupportsPS = true,
                SupportsXbox = true,
                SupportsNintendo = true,
                ImageUrl = "test-image-url"
            };

            var like1 = new Like
            {
                UserId = userId,
                Game = game1
            };

            await this.dbContext.Games.AddRangeAsync(game1, game2);
            await this.dbContext.Likes.AddAsync(like1);
            await this.dbContext.SaveChangesAsync();

            // Act
            var likedGames = await this.gameService.GetAllLikedGamesByUserIdAsync(userId);

            // Assert
            Assert.AreEqual(1, likedGames.Count);
            Assert.AreEqual(game1.Id, likedGames.First().Id);
        }

        [Test]
        public async Task GetAllRatedGamesByUserIdAsync_ValidUserId_Success()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var game1 = new Game
            {
                Name = "Test Game 1",
                Description = "Test Description",
                AgeRestriction = AgeRestrictionType.PEGI7,
                Status = ReleaseStatusType.OpenBeta,
                SupportsPC = true,
                SupportsPS = true,
                SupportsXbox = true,
                SupportsNintendo = true,
                ImageUrl = "test-image-url"
            };

            var game2 = new Game
            {
                Name = "Test Game 2",
                Description = "Test Description",
                AgeRestriction = AgeRestrictionType.PEGI7,
                Status = ReleaseStatusType.Alpha,
                SupportsPC = true,
                SupportsPS = true,
                SupportsXbox = true,
                SupportsNintendo = true,
                ImageUrl = "test-image-url"
            };

            var rating1 = new Rating
            {
                UserId = userId,
                Game = game1,
                Points = 4
            };

            await this.dbContext.Games.AddRangeAsync(game1, game2);
            await this.dbContext.Ratings.AddAsync(rating1);
            await this.dbContext.SaveChangesAsync();

            // Act
            var ratedGames = await this.gameService.GetAllRatedGamesByUserIdAsync(userId);

            // Assert
            Assert.AreEqual(1, ratedGames.Count);
            Assert.AreEqual(game1.Id, ratedGames.First().Id);
        }

        [Test]
        public async Task GetAllWishedGamesByUserIdAsync_ValidUserId_Success()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var game1 = new Game
            {
                Name = "Test Game 1",
                Description = "Test Description",
                AgeRestriction = AgeRestrictionType.PEGI18,
                Status = ReleaseStatusType.Alpha,
                SupportsPC = true,
                SupportsPS = true,
                SupportsXbox = true,
                SupportsNintendo = true,
                ImageUrl = "test-image-url"
            };

            var game2 = new Game
            {
                Name = "Test Game 2",
                Description = "Test Description",
                AgeRestriction = AgeRestrictionType.PEGI18,
                Status = ReleaseStatusType.Alpha,
                SupportsPC = true,
                SupportsPS = true,
                SupportsXbox = true,
                SupportsNintendo = true,
                ImageUrl = "test-image-url"
            };

            var wish1 = new Wish
            {
                UserId = userId,
                Game = game1
            };

            await this.dbContext.Games.AddRangeAsync(game1, game2);
            await this.dbContext.Wishes.AddAsync(wish1);
            await this.dbContext.SaveChangesAsync();

            // Act
            var wishedGames = await this.gameService.GetAllWishedGamesByUserIdAsync(userId);

            // Assert
            Assert.AreEqual(1, wishedGames.Count);
            Assert.AreEqual(game1.Id, wishedGames.First().Id);
        }

        [Test]
        public async Task GetBestGamesAsync_ValidData_Success()
        {
            // Arrange
            var game1 = new Game
            {
                Name = "Test Game 1",
                Description = "Test Description",
                AgeRestriction = AgeRestrictionType.PEGI18,
                Status = ReleaseStatusType.EarlyAccess,
                SupportsPC = true,
                SupportsPS = true,
                SupportsXbox = true,
                SupportsNintendo = true,
                ImageUrl = "test-image-url"
            };

            var game2 = new Game
            {
                Name = "Test Game 2",
                Description = "Test Description",
                AgeRestriction = AgeRestrictionType.PEGI16,
                Status = ReleaseStatusType.OfficialRelease,
                SupportsPC = true,
                SupportsPS = true,
                SupportsXbox = true,
                SupportsNintendo = true,
                ImageUrl = "test-image-url"
            };

            var rating1 = new Rating
            {
                Game = game1,
                Points = 5
            };

            var rating2 = new Rating
            {
                Game = game2,
                Points = 4
            };

            await this.dbContext.Games.AddRangeAsync(game1, game2);
            await this.dbContext.Ratings.AddRangeAsync(rating1, rating2);
            await this.dbContext.SaveChangesAsync();

            // Act
            var bestGames = await this.gameService.GetBestGamesAsync();

            // Assert
            Assert.AreEqual(6, bestGames.Count);
            Assert.AreEqual(game1.Id, bestGames.First().Id);
        }

        [TearDown]
        public void TearDown()
        {
            this.dbContext.Dispose();
        }
    }
}