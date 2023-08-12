using GoodGameDatabase.Data;
using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Services.Exceptions;
using GoodGameDatabase.Web.ViewModels.Guide;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GoodGameDatabase.Services.Data
{
    public class GuideService : IGuideService
    {
        private readonly ILogger<IGuideService> logger;
        private readonly ApplicationDbContext dbContext;

        public GuideService(ILogger<IGuideService> logger, ApplicationDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task<int> CreateNewGuideAsync(Guide guide)
        {
            try
            {
                await this.dbContext.Guides.AddAsync(guide);
                await this.dbContext.SaveChangesAsync();

                var currGuide = this.dbContext.Guides.FirstOrDefault(g => g.Id ==  guide.Id);

                return guide.Id;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while creating a new guide.");
                throw new ServiceException("An error occurred while creating a new guide. Please try again later.");
            }
        }

        public async Task DeleteGuideByIdAsync(int id)
        {
            try
            {
                var guideToDelete = await dbContext.Guides.FirstOrDefaultAsync(g => g.Id == id);

                if (guideToDelete != null)
                {
                    dbContext.Guides.Remove(guideToDelete);

                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new InvalidOperationException($"Guide with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while deleting a guide.");
                throw;
            }
        }

        public async Task EditGuideByIdAsync(int id, EditGuideViewModel viewModel)
        {
            try
            {
                Guide guide = await this.dbContext.Guides.FirstOrDefaultAsync(g => g.Id == id);
                if (guide == null)
                {
                    throw new ArgumentNullException($"Guide with ID {id} not found.");
                }

                // Update the guide properties using viewModel
                // ...

                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while editing the guide.");
                throw new ServiceException("An error occurred while editing the guide. Please try again later.");
            }
        }

        public async Task<ICollection<AllGuideViewModel>> GetAllGuidesAsync()
        {
            try
            {
                var guides = await this.dbContext.Guides
                    .Select(g => new AllGuideViewModel()
                    {
                        Id = g.Id,
                        Title = g.Title,
                        Description = g.Description,
                        Language = g.Language.ToString(),
                        Category = g.Category.ToString()
                    }).ToArrayAsync();

                return guides;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching all guides.");
                throw new ServiceException("An error occurred while fetching all guides. Please try again later.");
            }
        }

        public async Task<GuideDetailsViewModel> GetGuideDetailsByIdAsync(int id)
        {
            try
            {
                Guide guide = await this.dbContext.Guides
                    .Include(g => g.Game)
                    .Include(g => g.Writer)
                    .FirstOrDefaultAsync(g => g.Id == id);

                if (guide == null)
                {
                    throw new ArgumentNullException($"Guide with ID {id} not found.");
                }
                GuideDetailsViewModel detailGuide = new GuideDetailsViewModel
                {
                    Title = guide.Title,
                    Description = guide.Description,
                    ImageUrl = guide.Game.ImageUrl,
                    Language = guide.Language.ToString(),
                    Category = guide.Category.ToString(),
                    Game = guide.Game.Name,
                    Writer = guide.Writer.Email
                };

                return detailGuide;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching guide details.");
                throw new ServiceException("An error occurred while fetching guide details. Please try again later.");
            }
        }

        public async Task<int> GetTotalGuidesCount()
        {
            return await this.dbContext.Guides.CountAsync();
        }
    }
}
