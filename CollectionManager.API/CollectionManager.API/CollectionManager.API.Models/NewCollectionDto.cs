namespace CollectionManager.API.Models
{
    public class NewCollectionDto
    {
        public string? CollectionName { get; set; }
        public Guid AccountId { get; set; }
    }
}