using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Web.ViewModels.Review;

namespace GoodGameDatabase.Services.Data.Contracts
{
    public interface IReviewService
    {
        Task Create(string id, Review review);
        Task<ICollection<GameReviewViewModel>> GetAllGameReviews();
    }
}
