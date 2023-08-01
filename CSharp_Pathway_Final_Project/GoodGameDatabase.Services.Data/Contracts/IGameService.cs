using GoodGameDatabase.Web.ViewModels.Game;

namespace GoodGameDatabase.Services.Data.Contracts
{
    public interface IGameService
    {
        Task<ICollection<AllGameViewModel>> GetAllAsync();
        Task<GameDetailsViewModel> GetDetailsByIdAsync(int id);
        Task<ICollection<BestFiveGameViewModel>> GetBestFiveAsync();
        Task<EditGameViewModel> Edit(int id, EditGameViewModel viewModel);
    }
}
