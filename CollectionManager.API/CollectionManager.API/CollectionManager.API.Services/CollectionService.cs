using CollectionManager.API.Common.Utils;
using CollectionManager.API.Domain;
using CollectionManager.API.Models;
using CollectionManager.API.Repository;
using CollectionManager.API.Repository.Interfaces;
using CollectionManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Logging;

namespace CollectionManager.API.Services
{
    public class CollectionService : ICollectionService 
    {
        private readonly ICollectionRepository _collectionRepo;
        private readonly IHttpContextService _httpService;

        public CollectionService(ICollectionRepository collectionRepo, IHttpContextService httpService)
        {
            _collectionRepo = collectionRepo;
            _httpService = httpService;
        }

        public async Task<List<Collection>> GetCollections()
        {
            var accountId = _httpService.GetAccountId();
            return await _collectionRepo.GetCollections(accountId);
        }

        public async Task<Collection> CreateCollection(NewCollectionDto newCollection)
        {
            var accountId = _httpService.GetAccountId();
            newCollection.AccountId = accountId;
            var collection = ModelUtil.CreateCollectionModel(newCollection);
            await _collectionRepo.CreateCollection(collection);
            return collection;
        }   
    }
}