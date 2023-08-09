namespace GoodGameDatabase.Services.Data.Contracts
{
    public interface IUserService
    {
        Task<string> GetUsernameById(Guid id);
        Task<string> GetUsernameByEmail(Guid id);

    }
}
