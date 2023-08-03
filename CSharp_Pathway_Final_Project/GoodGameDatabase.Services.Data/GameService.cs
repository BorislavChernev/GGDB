using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Data;
using GoodGameDatabase.Web.ViewModels.Game;
using Microsoft.EntityFrameworkCore;
using GoodGameDatabase.Data.Model;
using System;
using GoodGameDatabase.Data.Model.Enums;

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
                .FirstAsync(g => g.Id == id);

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
            return await this.dbContext.Games
            .OrderByDescending(g => g.Rating)
            .Select(g => new BestFiveGameViewModel()
            {
                Id = g.Id,
                Name = g.Name,
                ReleaseDate = g.ReleaseDate.ToString(),
                ImageUrl = g.ImageUrl,
                Rating = g.Rating,
                SupportsPC = g.SupportsPC,
                SupportsPS = g.SupportsPS,
                SupportsXbox = g.SupportsXbox,
                SupportsNintendo = g.SupportsNintendo,
            })
            .Take(5)
            .ToArrayAsync();
        }

        public async Task<GameDetailsViewModel> GetDetailsByIdAsync(int id)
         {
            return await this.dbContext.Games
                .Where(g => g.Id == id)
                .Select(g => new GameDetailsViewModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    ReleaseDate = g.ReleaseDate.ToString(),
                    ImageUrl = g.ImageUrl,
                    Rating = g.Rating,
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
            return await this.dbContext.Games
            .Select(g => new AllGameViewModel()
            {
                Id = g.Id,
                Name = g.Name,
                ReleaseDate = g.ReleaseDate.ToString(),
                ImageUrl = g.ImageUrl,
                Rating = g.Rating,
                Description = g.Description,
                SupportsPC = g.SupportsPC,
                SupportsPS = g.SupportsPS,
                SupportsXbox = g.SupportsXbox,
                SupportsNintendo = g.SupportsNintendo,
            })
            .ToArrayAsync();
        }

        public async Task Create(Game game)
        {
            game.ReleaseDate = DateTime.UtcNow;
            game.CreatorId = 3;

            await dbContext.Games.AddAsync(game);
            await dbContext.SaveChangesAsync();
        }
    }
}
