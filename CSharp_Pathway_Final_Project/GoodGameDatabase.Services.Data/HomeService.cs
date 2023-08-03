using GoodGameDatabase.Data;
using GoodGameDatabase.Services.Data.Contracts;

namespace GoodGameDatabase.Services.Data
{
    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext dbContext;

        public HomeService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
