using GoodGameDatabase.Services.Data.Contracts;

namespace GoodGameDatabase.Services.Data
{
    public class UserService : IUserService
    {
        public Task<string> GetUsernameByEmail(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUsernameById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
