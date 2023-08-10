using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Web.ViewModels.Guide;

namespace GoodGameDatabase.Services.Data.Contracts
{
    public interface IGuideService
    {
        Task<int> CreateNewGuideAsync(Guide guide);
        Task EditGuideByIdAsync(int id, EditGuideViewModel viewModel);
        Task<ICollection<AllGuideViewModel>> GetAllGuidesAsync();
        Task<GuideDetailsViewModel> GetGuideDetailsByIdAsync(int id);
        Task DeleteGuideByIdAsync(int id);
    }
}
