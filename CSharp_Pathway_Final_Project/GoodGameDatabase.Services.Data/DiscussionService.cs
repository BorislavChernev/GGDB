using GoodGameDatabase.Data;
using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Services.Exceptions;
using GoodGameDatabase.Web.ViewModels.Discussion;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GoodGameDatabase.Services.Data
{
    public class DiscussionService : IDiscussionService
    {
        private readonly ILogger<IDiscussionService> logger;
        private readonly ApplicationDbContext dbContext;

        public DiscussionService(ILogger<IDiscussionService> logger, ApplicationDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<int> CreateNewAsync(Discussion discussion)
        {
            try
            {
                discussion.DatePosted = DateTime.UtcNow;

                await this.dbContext.Discussions.AddAsync(discussion);
                await this.dbContext.SaveChangesAsync();

                return discussion.Id;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while creating a new discussion.");
                throw new ServiceException("An error occurred while creating a new discussion. Please try again later.");
            }
        }

        public async Task DeleteDiscussionByIdAsync(int id)
        {
            try
            {
                var discussionsToDelete = await dbContext.Discussions.FirstOrDefaultAsync(d => d.Id == id);

                if (discussionsToDelete != null)
                {
                    dbContext.Discussions.Remove(discussionsToDelete);

                    var discussionMappingTableRecords = dbContext.DiscussionParticipants.Where(g => g.DiscussionId == id);
                    dbContext.DiscussionParticipants.RemoveRange(discussionMappingTableRecords);

                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new InvalidOperationException($"Discussion with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while deleting a guide.");
                throw;
            }
        }

        public async Task<ICollection<AllDiscussionViewModel>> GetAllAsync()
        {
            try
            {
                var discussions = await this.dbContext.Discussions
                    .Select(d => new AllDiscussionViewModel()
                    {
                        Id = d.Id,
                        Topic = d.Topic,
                        Description = d.Description,
                        DatePosted = d.DatePosted.ToString(),
                        pinned = d.pinned,
                    }).ToArrayAsync();

                return discussions;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching all discussions.");
                throw new ServiceException("An error occurred while fetching all discussions. Please try again later.");
            }
        }

        public async Task<ICollection<AllDiscussionViewModel>> GetBestThreeDiscussionsAsync()
        {
            try
            {
                var discussions = await this.dbContext.Discussions
                    .Select(d => new AllDiscussionViewModel()
                    {
                        Id = d.Id,
                        Topic = d.Topic,
                        Description = d.Description,
                        DatePosted = d.DatePosted.ToString(),
                        pinned = d.pinned
                    })
                    .Take(3)
                    .ToArrayAsync();

                return discussions;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching the best discussions.");
                throw new ServiceException("An error occurred while fetching the best discussions. Please try again later.");
            }
        }

        public async Task<DiscussionDetailsViewModel> GetDetailsByIdAsync(int id)
        {
            try
            {
                Discussion discussion = await this.dbContext.Discussions
                    .FirstOrDefaultAsync(d => d.Id == id);

                return new DiscussionDetailsViewModel()
                {
                    Id = discussion.Id,
                    Topic = discussion.Topic,
                    Description = discussion.Description,
                };
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching discussion details.");
                throw new ServiceException("An error occurred while fetching discussion details. Please try again later.");
            }
        }

        public async Task JoinUserByIdAsync(int discussionId, Guid userId)
        {
            try
            {
                await this.dbContext.DiscussionParticipants
                    .AddAsync(new DiscussionParticipant()
                    {
                        UserId = userId,
                        DiscussionId = discussionId
                    });

                await this.dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while joining the user to the discussion.");
                throw new ServiceException("An error occurred while joining the user to the discussion. Please try again later.");
            }
        }
    }
}
