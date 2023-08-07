using GoodGameDatabase.Data;
using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Discussion;
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

        public async Task<int> CreateNewAsync(Discussion discussion)
        {
            discussion.DatePosted = DateTime.UtcNow;

            await this.dbContext.Discussions.AddAsync(discussion);
            await this.dbContext.SaveChangesAsync();

            return discussion.Id;
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
            }).ToArrayAsync();
        }

        public async Task<DiscussionDetailsViewModel> GetDetailsByIdAsync(int id)
        {
            Discussion discussion = await this.dbContext.Discussions
                .FirstOrDefaultAsync(d => d.Id == id);
                
            return new DiscussionDetailsViewModel()
            {
                Id = discussion.Id,
                Topic = discussion.Topic,
                Description= discussion.Description,
                
            };
        }

        public async Task JoinUserByIdAsync(int discussionId, Guid userId)
        {
            await this.dbContext.DiscussionParticipants
                .AddAsync(new DiscussionParticipant()
                {
                    UserId = userId,
                    DiscussionId = discussionId
                });
        }
    }
}
