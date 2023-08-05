using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Web.ViewModels.Review;

namespace GoodGameDatabase.Services.Data.Contracts
{
    public interface IReviewService
    {
        Task Create(Review review);
        Task<ICollection<GameReviewViewModel>> GetAllGameReviews();
        Task<ICollection<GameReviewViewModel>> GetGameReviews(int gameId);
    }
}
