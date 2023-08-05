using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Web.ViewModels.Guide;

namespace GoodGameDatabase.Services.Data.Contracts
{
    public interface IGuideService
    {
        Task CreateNew(Guide guide);
        Task<ICollection<AllGuideViewModel>> GetAllAsync();
    }
}
