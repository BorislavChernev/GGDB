using GoodGameDatabase.Web.ViewModels.Game;

namespace GoodGameDatabase.Services.Data.Contracts
{
    public interface IGameService
    {
        Task<ICollection<AllGameViewModel>> GetAllAsync();
    }
}
