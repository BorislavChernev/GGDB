using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Web.ViewModels.Game;

namespace GoodGameDatabase.Services.Data.Contracts
{
    public interface IGameService
    {
        Task CreateNewGameAsync(Game game);
        Task EditGameByIdAsync(int id, EditGameViewModel viewModel);
        Task<ICollection<AllGameViewModel>> GetAllGamesAsync();
        Task<GameDetailsViewModel> GetGameDetailsByIdAsync(int id);
        Task LikeGameByIdAsync(int gameId, Guid userId);
        Task RateGameByIdAsync(int gameId, int rating, string userId);
        Task WishGameByIdAsync(int gameId, Guid userId);
        Task<ICollection<AllGameViewModel>> GetAllLikedGamesByUserIdAsync(Guid userId);
        Task<ICollection<AllGameViewModel>> GetAllRatedGamesByUserIdAsync(Guid userId);
        Task<ICollection<AllGameViewModel>> GetAllWishedGamesByUserIdAsync(Guid userId);
        Task<ICollection<BestSixGameViewModel>> GetBestGamesAsync();
        Task DeleteGameByIdAsync(int id);
        Task<int> GetTotalGamesCount();
    }
}
