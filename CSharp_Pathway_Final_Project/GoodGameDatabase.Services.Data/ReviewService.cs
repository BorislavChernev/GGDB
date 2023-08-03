using GoodGameDatabase.Data;
using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Review;
using Microsoft.EntityFrameworkCore;

namespace GoodGameDatabase.Services.Data
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext dbContext;

        public ReviewService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Create(string Id, Review review)
        {
            review.AuthorId = Guid.Parse(Id);
            await this.dbContext.Reviews.AddAsync(review);
         
            await this.dbContext.SaveChangesAsync();

            GameReviewViewModel[] asd = await this.dbContext.Reviews
                    .Select(r => new GameReviewViewModel()
                    {
                        Description = r.Description,
                        Author = $"{r.Author}",
                        Type = $"{r.Type}",
                    }).ToArrayAsync();
        }

        public async Task Delete(int id)
        {
            Review review = await this.dbContext.Reviews
                .FirstOrDefaultAsync(r => r.Id == id);

            this.dbContext.Remove(review);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<GameReviewViewModel>> GetAllGameReviews()
        {
            return await this.dbContext.Reviews
                .Select(r => new GameReviewViewModel()
                {
                    Description = r.Description,
                    Type = r.Type.ToString(),
                    Likes = r.Likes,
                    Dislikes = r.Dislikes,
                    Author = r.Author.NormalizedEmail
                    
                }).ToArrayAsync();
        }
    }
}
