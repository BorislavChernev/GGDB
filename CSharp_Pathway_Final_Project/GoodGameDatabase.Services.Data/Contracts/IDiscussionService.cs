using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Web.ViewModels.Discussion;

namespace GoodGameDatabase.Services.Data.Contracts
{
    public interface IDiscussionService
    {
        Task<ICollection<AllDiscussionViewModel>> GetAllAsync();
        Task JoinUserByIdAsync(int discussionId, Guid userId);
        Task<int> CreateNewAsync(Discussion discussion);
        Task<DiscussionDetailsViewModel> GetDetailsByIdAsync(int id);
    }
}
