using CollectionManager.API.Models;
using CollectionManager.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CollectionManager.API.Services
{
    public class ScryfallService : IScryfallService
    {
        private readonly HttpClient _httpClient;

        public ScryfallService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ScryfallAutocompleteResponse> AutocompleteAsync(string query)
        {

            var response = await _httpClient.GetFromJsonAsync<ScryfallAutocompleteResponse>($"cards/autocomplete?q={Uri.EscapeDataString(query)}");

            if (response is null)
            {
                throw new Exception("Failed to deserialize Scryfall response.");
            }

            return response; 
        }

    }
}
