using GoodGameDatabase.Web.ViewModels.Discussion;

namespace GoodGameDatabase.Services.Data.Contracts
{
    public interface IDiscussionService
    {
        Task<ICollection<AllDiscussionViewModel>> GetAllAsync();
    }
}
