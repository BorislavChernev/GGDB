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

        public async Task<EditGameViewModel> Edit(int id)
        {
            Game game = await this.dbContext.Games
                .FirstOrDefaultAsync(g => g.Id == id);

            EditGameViewModel viewModel = new EditGameViewModel()
            {
                Name = game.Name,
                Description = game.Description,
                Status = game.Status.ToString(),
                SupportsWindows = game.SupportsWindows,
                SupportsLinux = game.SupportsLinux,
                SupportsMacOs = game.SupportsMacOs,
                ImageUrl = game.ImageUrl
            };

            return viewModel;
        }

        //public async Task<EditGameViewModel> Edit(int id)
        //{
        //    Game game = await this.dbContext.Games
        //        .FirstOrDefaultAsync(g => g.Id == id);

        //    game.Name = editGameViewModel.Name;
        //    game.Description = editGameViewModel.Description;
        //    game.Status = Enum.Parse<ReleaseStatusType>(editGameViewModel.Status);
        //    game.SupportsWindows = editGameViewModel.SupportsWindows;
        //    game.SupportsMacOs = editGameViewModel.SupportsMacOs;
        //    game.SupportsLinux = editGameViewModel.SupportsLinux;
        //    game.ImageUrl = editGameViewModel.ImageUrl;

        //    dbContext.SaveChangesAsync();
        //}

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
