using CollectionManager.API.Domain;
using CollectionManager.API.Models;

namespace CollectionManager.API.Services.Interfaces
{
    public interface ICollectionService
    {
        public Task<List<Collection>> GetCollections(string userId);
        public Task<Collection> CreateCollection(NewCollectionDto newCollection);
    }
}