using CollectionManager.API.Domain;

namespace CollectionManager.API.Repository.Interfaces 
{
    public interface ICollectionRepository 
    {
        public Task<List<Collection>> GetCollections(Guid userId);
        public Task CreateCollection(Collection collection);
    }

}