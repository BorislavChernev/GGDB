using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Web.ViewModels.Guide;

namespace GoodGameDatabase.Services.Data.Contracts
{
    public interface IGuideService
    {
        Task<int> CreateNewGuideAsync(Guide guide);
        Task<ICollection<AllGuideViewModel>> GetAllGuidesAsync();
    }
}
