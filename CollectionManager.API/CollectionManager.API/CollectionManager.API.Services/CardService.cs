using CollectionManager.API.Models;
using CollectionManager.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionManager.API.Services
{
    public class CardService : ICardService
    {
        private readonly IScryfallService _scryfallService;

        public CardService(IScryfallService scryfallService)
        {
            _scryfallService = scryfallService;
        }

        public async Task<List<string>> AutocompleteAsync(string query)
        {
            var response = await _scryfallService.AutocompleteAsync(query);

            return response.Data;
        }
    }
}
