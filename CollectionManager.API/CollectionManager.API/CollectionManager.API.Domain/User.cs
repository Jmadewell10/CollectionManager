using System.ComponentModel.DataAnnotations.Schema;

namespace CollectionManager.API.Domain
{
    public class User
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public Guid AccountId { get; set; }
        public Account? Account { get; set; }

    }
}
