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
        private readonly IAccountService _accountService;

        public CollectionService(ICollectionRepository collectionRepo, IAccountService accountService)
        {
            _collectionRepo = collectionRepo;
            _accountService = accountService;
        }

        public async Task<List<Collection>> GetCollections(string userId)
        {
            return await _collectionRepo.GetCollections(userId);
        }

        public async Task<Collection> CreateCollection(NewCollectionDto newCollection)
        {
            var account = await _accountService.GetAccountFromToken();
            newCollection.AccountId = account.AccountId;
            var collection = ModelUtil.CreateCollectionModel(newCollection);
            await _collectionRepo.CreateCollection(collection);
            return collection;
        }   
    }
}