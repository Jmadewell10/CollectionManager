using CollectionManager.API.Domain;
using CollectionManager.API.Models;

namespace CollectionManager.API.Services.Interfaces
{
    public interface IHttpContextService
    {
        Task<string> GenerateToken(Account account);
        Guid GetAccountId();
    }
}