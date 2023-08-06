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
        public async Task Edit(int id, EditGameViewModel viewModel)
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

        public async Task<ICollection<BestFiveGameViewModel>> GetBestFiveAsync()
        {
            var games = await this.dbContext.Games
                .Include(g => g.Ratings)
                .Include(g => g.Likes)
                .Select(g => new BestFiveGameViewModel
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

            BestFiveGameViewModel[] asd = games
                .OrderByDescending(g => g.Rating)
                .Take(5)
                .ToArray();

            return asd;
        }

        public async Task<GameDetailsViewModel> GetDetailsByIdAsync(int id)
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

        public async Task<ICollection<AllGameViewModel>> GetAllAsync()
        {
            var games = await this.dbContext.Games
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

            return games;
        }

        public async Task Create(Game game)
        {
            game.ReleaseDate = DateTime.UtcNow;
            game.CreatorId = 3;

            await dbContext.Games.AddAsync(game);
            await dbContext.SaveChangesAsync();
        }

        public async Task Rate(int gameId, int points, string userId)
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

        public async Task<double> GetRating(int gameId)
        {
            //Game game = await this.dbContext.Games
            //    .FirstAsync(g => g.Id == gameId);

            //Rating rating = new Rating();
            //double points = 0;
            //try
            //{
            //    rating = await this.dbContext.Ratings
            //    .FirstAsync(r => r.GameId == gameId);
            //    points = rating.Points;

            //    points = game.Rating;
            //}
            //catch (Exception)
            //{                
            //}

            //return points;

            throw new NotImplementedException();
        }

        public async Task Like(int gameId, string userId)
        {
            Game game = await this.dbContext.Games
            .Include(g => g.Likes)
            .FirstAsync(g => g.Id == gameId);

            Like like = game.Likes
                .FirstOrDefault(l => l.UserId == Guid.Parse(userId));

            if (like == null)
            {
                like = new Like()
                {
                    GameId = gameId,
                    UserId = Guid.Parse(userId),
                };

                game.Likes.Add(like);
            } else game.Likes.Remove(like);

            await dbContext.SaveChangesAsync();
        }
    }
}
