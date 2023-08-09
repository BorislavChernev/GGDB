using GoodGameDatabase.Data;
using GoodGameDatabase.Data.Model;
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

        public async Task<int> CreateNewGuideAsync(Guide guide)
        {
            await this.dbContext.Guides.AddAsync(guide);
            await this.dbContext.SaveChangesAsync();

            return guide.Id;
        }

        public Task EditGuideByIdAsync(int id, EditGuideViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<AllGuideViewModel>> GetAllGuidesAsync()
        {
            var guides = await this.dbContext.Guides.ToArrayAsync();

            return await this.dbContext.Guides
                .Select(g => new AllGuideViewModel()
                {
                    Id = g.Id,
                    Title = g.Title,
                    Description = g.Description,
                    Language = g.Language.ToString(),
                    Category = g.Category.ToString()
                }).ToArrayAsync();
        }

        public Task<GuideDetailsViewModel> GetGuideDetailsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
