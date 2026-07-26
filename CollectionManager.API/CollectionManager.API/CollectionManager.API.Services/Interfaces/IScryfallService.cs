using CollectionManager.API.Models;

namespace CollectionManager.API.Services.Interfaces
{
    public interface IScryfallService
    {
        Task<ScryfallAutocompleteResponse> AutocompleteAsync(string query);
    }
}