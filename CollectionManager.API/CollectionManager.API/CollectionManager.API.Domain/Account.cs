using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace CollectionManager.API.Domain
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
        public ICollection<Collection>? Collections { get; set; }
        public User? User { get; set; }

    }
}
