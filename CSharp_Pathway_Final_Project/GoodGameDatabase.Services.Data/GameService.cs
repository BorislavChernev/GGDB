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
                .Select(g => new BestFiveGameViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    ReleaseDate = g.ReleaseDate.ToString(),
                    ImageUrl = g.ImageUrl,
                    SupportsPC = g.SupportsPC,
                    SupportsPS = g.SupportsPS,
                    SupportsXbox = g.SupportsXbox,
                    SupportsNintendo = g.SupportsNintendo,
                })
                .ToArrayAsync();

            var allBestFiveGameViewModels = new List<BestFiveGameViewModel>();

            foreach (var game in games)
            {
                int rating = await this.GetRating(game.Id);
                game.Rating = rating;
                allBestFiveGameViewModels.Add(game);
            }

            return allBestFiveGameViewModels
                .OrderByDescending(g => g.Rating)
                .Take(5)
                .ToArray();
        }

        public async Task<GameDetailsViewModel> GetDetailsByIdAsync(int id)
        {
            int rating = await this.GetRating(id);

            return await this.dbContext.Games
                .Where(g => g.Id == id)
                .Select(g => new GameDetailsViewModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    ReleaseDate = g.ReleaseDate.ToString(),
                    ImageUrl = g.ImageUrl,
                    Rating = rating,
                    Status = g.Status.ToString(),
                    Description = g.Description,
                    SupportsPC = g.SupportsPC,
                    SupportsPS = g.SupportsPS,
                    SupportsXbox = g.SupportsXbox,
                    SupportsNintendo = g.SupportsNintendo,

                })
                .FirstAsync();
        }

        public async Task<ICollection<AllGameViewModel>> GetAllAsync()
        {
            var games = await this.dbContext.Games
                .Select(g => new AllGameViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    ReleaseDate = g.ReleaseDate.ToString(),
                    ImageUrl = g.ImageUrl,
                    Description = g.Description,
                    SupportsPC = g.SupportsPC,
                    SupportsPS = g.SupportsPS,
                    SupportsXbox = g.SupportsXbox,
                    SupportsNintendo = g.SupportsNintendo,
                })
                .ToArrayAsync();

            var allGameViewModels = new List<AllGameViewModel>();

            foreach (var game in games)
            {
                int rating = await this.GetRating(game.Id);
                game.Rating = rating;
                allGameViewModels.Add(game);
            }

            return allGameViewModels;
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
            Rating review = new Rating()
            {
                GameId = gameId,
                UserId = Guid.Parse(userId),
                Points = points
            };

            await this.dbContext.Ratings.AddAsync(review);
        }

        public async Task<int> GetRating(int gameId)
        {
            Rating rating = new Rating();
            int points = 0;
            try
            {
                rating = await this.dbContext.Ratings
                .FirstAsync(r => r.GameId == gameId);
                points = rating.Points;
            }
            catch (Exception)
            {                
            }
            return points;
        }
    }
}
