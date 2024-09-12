using CollectionManager.API.Models;

namespace CollectionManager.API.Services.Interfaces
{
    public interface IAccountService
    {
        Task<string> AddAccount(NewAccountDto accountDto);
        Task<(string, bool)> Authenticate(LoginCredentialsDto loginCredentials);
    }
}