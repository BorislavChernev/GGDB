using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Data;
using GoodGameDatabase.Web.Models.Game;

namespace GoodGameDatabase.Services.Data
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext dbContext;

        public GameService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<ICollection<AllGameViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
