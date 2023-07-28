using GoodGameDatabase.Data;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Guide;
using Microsoft.EntityFrameworkCore;

namespace GoodGameDatabase.Services.Data
{
    public class GuideService : IGuideService
    {
        private readonly ApplicationDbContext dbContext;

        public GuideService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ICollection<AllGuideViewModel>> GetAllAsync()
        {
            return await this.dbContext.Guides
                .Select(g => new AllGuideViewModel()
                {
                    Id = g.Id,
                    Title = g.Title,
                    Rating = g.Rating,
                    Language = g.Language.ToString(),
                    Category = g.Category.ToString(),
                }).ToArrayAsync();
        }
    }
}
