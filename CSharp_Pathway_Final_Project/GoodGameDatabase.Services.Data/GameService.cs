using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Data;
using GoodGameDatabase.Web.ViewModels.Game;
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
            }).ToArrayAsync();
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
                }).FirstAsync();
        }
    }
}
