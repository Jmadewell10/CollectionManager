using CollectionManager.API.Domain;
using CollectionManager.API.Models;

namespace CollectionManager.API.Common.Utils
{
    public static class ModelUtil
    {

         public static Collection CreateCollectionModel(NewCollectionDto dto)
        {
            return new Collection
            {
                CollectionId = Guid.NewGuid(),
                CollectionName = dto.CollectionName,
                AccountId = dto.AccountId
            };
        }
    }
}
