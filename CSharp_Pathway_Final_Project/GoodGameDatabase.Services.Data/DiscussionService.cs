using GoodGameDatabase.Data;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Discussion;
using GoodGameDatabase.Web.ViewModels.Game;
using Microsoft.EntityFrameworkCore;

namespace GoodGameDatabase.Services.Data
{
    public class DiscussionService : IDiscussionService
    {
        private readonly ApplicationDbContext dbContext;

        public DiscussionService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ICollection<AllDiscussionViewModel>> GetAllAsync()
        {
            return await this.dbContext.Discussions
            .Select(d => new AllDiscussionViewModel()
            {
                Id = d.Id,
                Topic = d.Topic,
                Description = d.Description,
                DatePosted = d.DatePosted.ToString(),
                pinned = d.pinned,
                ReviewsCount = d.Reviews.Count(),
            }).ToArrayAsync();
        }

        public async Task<DiscussionDetailsViewModel> GetDetailsByIdAsync(int id)
        {
            return await this.dbContext.Discussions
            .Where(d => d.Id == id)
            .Select(d => new DiscussionDetailsViewModel()
            {
                Id = d.Id,
                Topic = d.Topic,
                Description = d.Description,
                DatePosted = d.DatePosted.ToString(),
                pinned = d.pinned,
                ReviewsCount = d.Reviews.Count(),
            }).FirstAsync();
        }
    }
}
