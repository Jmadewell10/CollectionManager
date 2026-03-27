using CollectionManager.API.Common.Utils;
using CollectionManager.API.Domain;
using CollectionManager.API.Models;
using CollectionManager.API.Repository;
using CollectionManager.API.Repository.Interfaces;
using CollectionManager.API.Services.Interfaces;
using Microsoft.IdentityModel.Logging;

namespace CollectionManager.API.Services
{
    public class CollectionService : ICollectionService 
    {
        private readonly ICollectionRepository _collectionRepo;

        public CollectionService(CollectionRepository collectionRepo)
        {
            _collectionRepo = collectionRepo;   
        }

        public async Task<List<Collection>> GetCollections(string userId)
        {
            return await _collectionRepo.GetCollections(userId);
        }

        public async Task<Collection> CreateCollection(NewCollectionDto newCollection)
        {
            var collection = ModelUtil.CreateCollectionModel(newCollection);
            await _collectionRepo.CreateCollection(collection);
            return collection;
        }   
    }
}