using GoodGameDatabase.Web.ViewModels.Discussion;
using GoodGameDatabase.Web.ViewModels.Game;

namespace GoodGameDatabase.Services.Data.Contracts
{
    public interface IDiscussionService
    {
        Task<ICollection<AllDiscussionViewModel>> GetAllAsync();
        Task<DiscussionDetailsViewModel> GetDetailsByIdAsync(int id);
    }
}
