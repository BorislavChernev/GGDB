using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Data;
using GoodGameDatabase.Web.ViewModels.Game;
using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Data.Model.Enums;

using Microsoft.EntityFrameworkCore;

namespace GoodGameDatabase.Services.Data
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext dbContext;

        public GameService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateNewGameAsync(Game game)
        {
            game.ReleaseDate = DateTime.UtcNow;

            await dbContext.Games.AddAsync(game);
            await dbContext.SaveChangesAsync();
        }
        public async Task EditGameByIdAsync(int id, EditGameViewModel viewModel)
        {
            Game game = await this.dbContext.Games
                .FirstOrDefaultAsync(g => g.Id == id);

            game.Name = viewModel.Name;
            game.Description = viewModel.Description;
            game.Status = Enum.Parse<ReleaseStatusType>(viewModel.Status);
            game.SupportsPC = viewModel.SupportsPC;
            game.SupportsPS = viewModel.SupportsPS;
            game.SupportsXbox = viewModel.SupportsXbox;
            game.SupportsNintendo = viewModel.SupportsNintendo;
            game.ImageUrl = viewModel.ImageUrl;

            await dbContext.SaveChangesAsync();
        }
        public async Task<ICollection<AllGameViewModel>> GetAllGamesAsync()
        {
            return await this.dbContext.Games
                .Include(g => g.Ratings)
                .Include(g => g.Likes)
                .Select(g => new AllGameViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    ReleaseDate = g.ReleaseDate.ToString(),
                    ImageUrl = g.ImageUrl,
                    Description = g.Description,
                    Rating = g.Rating,
                    Likes = g.LikePoints,
                    SupportsPC = g.SupportsPC,
                    SupportsPS = g.SupportsPS,
                    SupportsXbox = g.SupportsXbox,
                    SupportsNintendo = g.SupportsNintendo,
                })
                .ToArrayAsync();
        }
        public async Task<GameDetailsViewModel> GetGameDetailsByIdAsync(int id)
        {
            GameDetailsViewModel game = await this.dbContext.Games
                .Include(g => g.Likes)
                .Include(g => g.Ratings)
                .Where(g => g.Id == id)
                .Select(g => new GameDetailsViewModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    ReleaseDate = g.ReleaseDate.ToString(),
                    ImageUrl = g.ImageUrl,
                    Status = g.Status.ToString(),
                    Rating = g.Rating,
                    Likes = g.LikePoints,
                    Description = g.Description,
                    SupportsPC = g.SupportsPC,
                    SupportsPS = g.SupportsPS,
                    SupportsXbox = g.SupportsXbox,
                    SupportsNintendo = g.SupportsNintendo,

                })
                .FirstAsync();

            return game;
        }
        public async Task LikeGameByIdAsync(int gameId, Guid userId)
        {
            Game game = await this.dbContext.Games
            .Include(g => g.Likes)
            .FirstAsync(g => g.Id == gameId);

            Like like = game.Likes
                .FirstOrDefault(l => l.UserId == userId);

            if (like == null)
            {
                like = new Like()
                {
                    GameId = gameId,
                    UserId = userId,
                };

                game.Likes.Add(like);
            } else game.Likes.Remove(like);

            await dbContext.SaveChangesAsync();
        }
        public async Task RateGameByIdAsync(int gameId, int points, string userId)
        {
            Game game = await this.dbContext.Games
                .Include(g => g.Ratings) // Eager load the Ratings collection
                .FirstAsync(g => g.Id == gameId);

            Rating rating = game.Ratings
            .FirstOrDefault(r => r.UserId == Guid.Parse(userId));

            if (rating == null)
            {
                rating = new Rating()
                {
                    GameId = gameId,
                    UserId = Guid.Parse(userId),
                    Points = points
                };

                game.Ratings.Add(rating);
            } else rating.Points = points;

            await dbContext.SaveChangesAsync();
        }
        public async Task WishGameByIdAsync(int gameId, Guid userId)
        {
            Game game = await this.dbContext.Games
            .Include(g => g.Wishes)
            .FirstAsync(g => g.Id == gameId);

            Wish wish = game.Wishes
                .FirstOrDefault(l => l.UserId == userId);

            if (wish == null)
            {
                wish = new Wish()
                {
                    GameId = gameId,
                    UserId = userId,
                };

                game.Wishes.Add(wish);
            }
            else game.Wishes.Remove(wish);

            await dbContext.SaveChangesAsync();
        }
        public async Task<ICollection<AllGameViewModel>> GetAllLikedGamesByUserIdAsync(Guid userId)
        {
            return await this.dbContext.Games
                .Where(g => g.Likes.Any(l => l.UserId == userId))
                .Include(g => g.Ratings)
                .Include(g => g.Likes)
                .Select(g => new AllGameViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    ReleaseDate = g.ReleaseDate.ToString(),
                    ImageUrl = g.ImageUrl,
                    Description = g.Description,
                    Rating = g.Rating,
                    Likes = g.LikePoints,
                    SupportsPC = g.SupportsPC,
                    SupportsPS = g.SupportsPS,
                    SupportsXbox = g.SupportsXbox,
                    SupportsNintendo = g.SupportsNintendo,
                })
                .ToArrayAsync();
        }
        public async Task<ICollection<AllGameViewModel>> GetAllRatedGamesByUserIdAsync(Guid userId)
        {
            return await this.dbContext.Games
                .Where(g => g.Ratings.Any(l => l.UserId == userId))
                .Include(g => g.Ratings)
                .Include(g => g.Likes)
                .Select(g => new AllGameViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    ReleaseDate = g.ReleaseDate.ToString(),
                    ImageUrl = g.ImageUrl,
                    Description = g.Description,
                    Rating = g.Rating,
                    Likes = g.LikePoints,
                    SupportsPC = g.SupportsPC,
                    SupportsPS = g.SupportsPS,
                    SupportsXbox = g.SupportsXbox,
                    SupportsNintendo = g.SupportsNintendo,
                })
                .ToArrayAsync();
        }
        public async Task<ICollection<AllGameViewModel>> GetAllWishedGamesByUserIdAsync(Guid userId)
        {
            return await this.dbContext.Games
                .Where(g => g.Wishes.Any(w => w.UserId == userId))
                .Include(g => g.Ratings)
                .Include(g => g.Likes)
                .Select(g => new AllGameViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    ReleaseDate = g.ReleaseDate.ToString(),
                    ImageUrl = g.ImageUrl,
                    Description = g.Description,
                    Rating = g.Rating,
                    Likes = g.LikePoints,
                    SupportsPC = g.SupportsPC,
                    SupportsPS = g.SupportsPS,
                    SupportsXbox = g.SupportsXbox,
                    SupportsNintendo = g.SupportsNintendo,
                })
                .ToArrayAsync();
        }
        public async Task<ICollection<BestSixGameViewModel>> GetBestGamesAsync()
        {
            var games = await this.dbContext.Games
                .Include(g => g.Ratings)
                .Include(g => g.Likes)
                .Select(g => new BestSixGameViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    ReleaseDate = g.ReleaseDate.ToString(),
                    ImageUrl = g.ImageUrl,
                    Rating = g.Rating,
                    Likes = g.LikePoints,
                    SupportsPC = g.SupportsPC,
                    SupportsPS = g.SupportsPS,
                    SupportsXbox = g.SupportsXbox,
                    SupportsNintendo = g.SupportsNintendo,
                })
                .ToArrayAsync();

            BestSixGameViewModel[] asd = games
                .OrderByDescending(g => g.Rating)
                .Take(6)
                .ToArray();

            return asd;
        }
    }
}
