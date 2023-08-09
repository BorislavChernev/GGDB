using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Web.ViewModels.Review;

namespace GoodGameDatabase.Services.Data.Contracts
{
    public interface IReviewService
    {
        Task CreateNewReviewAsync(Review review);
        Task DeleteReviewByIdAsync(int id);
        Task<ICollection<GameReviewViewModel>> GetAllGameReviewsAsync();
        Task<ICollection<GameReviewViewModel>> GetAllGameReviewsByIdAsync(int gameId);
    }
}
