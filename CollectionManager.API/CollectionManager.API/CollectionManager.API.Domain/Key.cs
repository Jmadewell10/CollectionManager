namespace CollectionManager.API.Domain
{
    public class Key
    {
        public Guid KeyId { get; set; }
        public Guid AccountId { get; set; }
        public string? Salt { get; set; }
        public Account? Account { get; set; }
    }
}
