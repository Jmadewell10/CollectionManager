using CollectionManager.API.Domain;

namespace CollectionManager.API.Repository.Interfaces
{
    public interface IAccountRepository
    {
        Task<string> AddAccount(Account account, User user, Key key);
        Task<IEnumerable<string>> GetAllUserNames();
        Task<(string, string)> GetKey(string userName);
    }
}