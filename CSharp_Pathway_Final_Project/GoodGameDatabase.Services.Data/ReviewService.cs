using GoodGameDatabase.Data;
using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Services.Exceptions;
using GoodGameDatabase.Web.ViewModels.Review;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GoodGameDatabase.Services.Data
{
    public class ReviewService : IReviewService
    {
        private readonly ILogger<IReviewService> logger;
        private readonly ApplicationDbContext dbContext;

        public ReviewService(ILogger<IReviewService> logger, ApplicationDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        public async Task CreateNewReviewAsync(Review review)
        {
            try
            {
                await this.dbContext.Reviews.AddAsync(review);
                await this.dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while creating a new review.");
                throw new ServiceException("An error occurred while creating a new review. Please try again later.");
            }
        }

        public async Task DeleteReviewByIdAsync(int id)
        {
            try
            {
                Review review = await this.dbContext.Reviews.FirstOrDefaultAsync(r => r.Id == id);
                if (review == null)
                {
                    throw new ArgumentNullException($"Review with ID {id} not found.");
                }

                this.dbContext.Remove(review);
                await this.dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while deleting the review.");
                throw new ServiceException("An error occurred while deleting the review. Please try again later.");
            }
        }

        public async Task<ICollection<GameReviewViewModel>> GetAllGameReviewsAsync()
        {
            try
            {
                var reviews = await this.dbContext.Reviews
                    .Select(r => new GameReviewViewModel()
                    {
                        Description = r.Description,
                        Type = r.Type.ToString(),
                        Likes = r.Likes,
                        Dislikes = r.Dislikes,
                        Author = r.Author.NormalizedEmail
                    }).ToArrayAsync();

                return reviews;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching all game reviews.");
                throw new ServiceException("An error occurred while fetching all game reviews. Please try again later.");
            }
        }

        public async Task<ICollection<GameReviewViewModel>> GetAllGameReviewsByIdAsync(int gameId)
        {
            try
            {
                var reviews = await this.dbContext.Reviews
                    .Where(r => r.GameId == gameId)
                    .Select(r => new GameReviewViewModel()
                    {
                        Description = r.Description,
                        Type = r.Type.ToString(),
                        Likes = r.Likes,
                        Dislikes = r.Dislikes,
                        Author = r.Author.NormalizedEmail
                    }).ToArrayAsync();

                return reviews;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching game reviews by ID.");
                throw new ServiceException("An error occurred while fetching game reviews by ID. Please try again later.");
            }
        }
    }
}
