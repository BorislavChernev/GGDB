using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Data;
using GoodGameDatabase.Web.ViewModels.Game;
using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Data.Model.Enums;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GoodGameDatabase.Services.Exceptions;

namespace GoodGameDatabase.Services.Data
{
    public class GameService : IGameService
    {
        private readonly ILogger<IGameService> logger;
        private readonly ApplicationDbContext dbContext;

        public GameService(ILogger<IGameService> logger, ApplicationDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task CreateNewGameAsync(Game game)
        {
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game), "Game cannot be null.");
            }

            try
            {
                game.ReleaseDate = DateTime.UtcNow;

                await dbContext.Games.AddAsync(game);
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                logger.LogError(ex, "Error occurred while saving the game to the database.");

                throw new ServiceException("An error occurred while saving the game. Please try again later.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unexpected error occurred.");

                throw new ServiceException("Something went wrong. Try again later.");
            }
        }

        public async Task EditGameByIdAsync(int id, EditGameViewModel viewModel)
        {
            try
            {
                Game game = await this.dbContext.Games
                    .FirstOrDefaultAsync(g => g.Id == id);

                if (game == null)
                {
                    throw new ArgumentNullException(nameof(game), "Game cannot be null.");
                }

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
            catch (Exception ex)
            {
                // Log the exception
                logger.LogError(ex, "An error occurred while editing the game.");

                // You can throw a custom ServiceException here if you want
                throw new ServiceException("An error occurred while editing the game. Please try again later.");
            }
        }

        public async Task<ICollection<AllGameViewModel>> GetAllGamesAsync()
        {
            try
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
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching all games.");
                throw new ServiceException("An error occurred while fetching all games. Please try again later.");
            }
        }

        public async Task<GameDetailsViewModel> GetGameDetailsByIdAsync(int id)
        {
            try
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
            catch (InvalidOperationException ex)
            {
                this.logger.LogError(ex, "Game with the specified ID was not found.");
                throw new ArgumentNullException("Game cannot be null.");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching game details.");
                throw new ServiceException("An error occurred while fetching game details. Please try again later.");
            }
        }

        public async Task LikeGameByIdAsync(int gameId, Guid userId)
        {
            try
            {
                Game game = await this.dbContext.Games
                    .Include(g => g.Likes)
                    .FirstAsync(g => g.Id == gameId);

                if (game == null)
                {
                    throw new ArgumentNullException(nameof(game), "Game not found.");
                }

                Like like = game.Likes.FirstOrDefault(l => l.UserId == userId);

                if (like == null)
                {
                    like = new Like()
                    {
                        GameId = gameId,
                        UserId = userId,
                    };

                    game.Likes.Add(like);
                }
                else
                {
                    game.Likes.Remove(like);
                }

                await dbContext.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                this.logger.LogError(ex, "Argument is null.");
                throw new ServiceException("An argument is null.");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while processing the like.");
                throw new ServiceException("An error occurred while processing the like. Please try again later.");
            }
        }

        public async Task RateGameByIdAsync(int gameId, int points, string userId)
        {
            try
            {
                Game game = await this.dbContext.Games
                    .Include(g => g.Ratings)
                    .FirstAsync(g => g.Id == gameId);

                if (game == null)
                {
                    throw new ArgumentNullException(nameof(game), "Game not found.");
                }

                Rating rating = game.Ratings.FirstOrDefault(r => r.UserId == Guid.Parse(userId));

                if (rating == null)
                {
                    rating = new Rating()
                    {
                        GameId = gameId,
                        UserId = Guid.Parse(userId),
                        Points = points
                    };

                    game.Ratings.Add(rating);
                }
                else
                {
                    rating.Points = points;
                }

                await dbContext.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                this.logger.LogError(ex, "Argument is null.");
                throw new ServiceException("An argument is null.");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while processing the rating.");
                throw new ServiceException("An error occurred while processing the rating. Please try again later.");
            }
        }

        public async Task WishGameByIdAsync(int gameId, Guid userId)
        {
            try
            {
                Game game = await this.dbContext.Games
                    .Include(g => g.Wishes)
                    .FirstAsync(g => g.Id == gameId);

                if (game == null)
                {
                    throw new ArgumentNullException(nameof(game), "Game not found.");
                }

                Wish wish = game.Wishes.FirstOrDefault(l => l.UserId == userId);

                if (wish == null)
                {
                    wish = new Wish()
                    {
                        GameId = gameId,
                        UserId = userId,
                    };

                    game.Wishes.Add(wish);
                }
                else
                {
                    game.Wishes.Remove(wish);
                }

                await dbContext.SaveChangesAsync();
            }
            catch (ArgumentNullException ex)
            {
                this.logger.LogError(ex, "Argument is null.");
                throw new ServiceException("An argument is null.");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while processing the wish.");
                throw new ServiceException("An error occurred while processing the wish. Please try again later.");
            }
        }

        public async Task<ICollection<AllGameViewModel>> GetAllLikedGamesByUserIdAsync(Guid userId)
        {
            try
            {
                var games = await this.dbContext.Games
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

                return games;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching liked games.");
                throw new ServiceException("An error occurred while fetching liked games. Please try again later.");
            }
        }

        public async Task<ICollection<AllGameViewModel>> GetAllRatedGamesByUserIdAsync(Guid userId)
        {
            try
            {
                var games = await this.dbContext.Games
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

                return games;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching rated games.");
                throw new ServiceException("An error occurred while fetching rated games. Please try again later.");
            }
        }

        public async Task<ICollection<AllGameViewModel>> GetAllWishedGamesByUserIdAsync(Guid userId)
        {
            try
            {
                var games = await this.dbContext.Games
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

                return games;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching wished games.");
                throw new ServiceException("An error occurred while fetching wished games. Please try again later.");
            }
        }

        public async Task<ICollection<BestSixGameViewModel>> GetBestGamesAsync()
        {
            try
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
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching best games.");
                throw new ServiceException("An error occurred while fetching best games. Please try again later.");
            }
        }
    }
}
