using CollectionManager.API.Domain;
using CollectionManager.API.Domain.Context;
using CollectionManager.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.API.Repository 
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly CollectionManagerContext _ctx;

        public CollectionRepository(CollectionManagerContext ctx)
        {
           _ctx = ctx; 
        }

        public async Task<List<Collection>> GetCollections(Guid userId)
        {
            try
            {
                return await _ctx.Collections
                        .Where(x => x.AccountId == userId)
                        .ToListAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine($"{e.Message}");
                throw;
            }
        }

        public async Task CreateCollection(Collection collection)
        {
            try
            {
                await _ctx.Collections.AddAsync(collection);
                await _ctx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
                throw;
            }
        }

    }
}