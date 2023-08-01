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
        public async Task<EditGameViewModel> Edit(int id, EditGameViewModel viewModel)
        {
            Game game = await this.dbContext.Games
                .FirstOrDefaultAsync(g => g.Id == id);

            game.Name = viewModel.Name;
            game.Description = viewModel.Description;
            game.Status = Enum.Parse<ReleaseStatusType>(viewModel.Status);
            game.SupportsWindows = viewModel.SupportsWindows;
            game.SupportsMacOs = viewModel.SupportsMacOs;
            game.SupportsLinux = viewModel.SupportsLinux;
            game.ImageUrl = viewModel.ImageUrl;

            await dbContext.SaveChangesAsync();
            return viewModel;
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
                SupportsWindows = g.SupportsWindows,
                SupportsLinux = g.SupportsLinux,
                SupportsMacOs = g.SupportsMacOs,
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
                    SupportsWindows = g.SupportsWindows,
                    SupportsLinux = g.SupportsLinux,
                    SupportsMacOs = g.SupportsMacOs,
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
                SupportsWindows = g.SupportsWindows,
                SupportsLinux = g.SupportsLinux,
                SupportsMacOs = g.SupportsMacOs,
            })
            .ToArrayAsync();
        }
    }
}
