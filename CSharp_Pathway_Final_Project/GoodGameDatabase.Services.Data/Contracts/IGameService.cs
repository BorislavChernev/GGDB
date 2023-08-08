using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Web.ViewModels.Game;

namespace GoodGameDatabase.Services.Data.Contracts
{
    public interface IGameService
    {
        Task<ICollection<AllGameViewModel>> GetAllAsync();
        Task<GameDetailsViewModel> GetDetailsByIdAsync(int id);
        Task<ICollection<BestSixGameViewModel>> GetBestSixGamesAsync();
        Task Edit(int id, EditGameViewModel viewModel);
        Task Create(Game game);
        Task Rate(int gameId, int rating, string userId);
        Task Like(int gameId, Guid userId);
        Task Wish(int gameId, Guid userId);
        Task<ICollection<AllGameViewModel>> GetAllLikedGamesByUserIdAsync(Guid userId);
        Task<ICollection<AllGameViewModel>> GetAllRatedGamesByUserIdAsync(Guid userId);
        Task<ICollection<AllGameViewModel>> GetAllWishedGamesByUserIdAsync(Guid userId);
    }
}
