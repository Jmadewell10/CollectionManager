namespace CollectionManager.API.Domain
{
    public class Account
    {
        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
        public Guid KeyId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public ICollection<Collection>? Collections { get; set; }
        public User? User { get; set; }
        public Key? Key { get; set; }

    }
}
