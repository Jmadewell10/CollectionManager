namespace CollectionManager.API.Models
{
    public class NewAccountDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public LoginCredentialsDto? Login { get; set; }
    }
}
