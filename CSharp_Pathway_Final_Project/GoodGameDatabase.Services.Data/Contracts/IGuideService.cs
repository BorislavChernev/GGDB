using GoodGameDatabase.Web.ViewModels.Guide;

namespace GoodGameDatabase.Services.Data.Contracts
{
    public interface IGuideService
    {
        Task<ICollection<AllGuideViewModel>> GetAllAsync();
    }
}
