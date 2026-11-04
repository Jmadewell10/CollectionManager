namespace CollectionManager.API.Services.Interfaces
{
    public interface ICardService
    {
        Task<List<string>> AutocompleteAsync(string query);
    }
}