using CollectionManager.API.Models;
using CollectionManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet("Autocomplete")]
        public async Task<ActionResult<List<string>>> Autocomplete([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return BadRequest();
            }
            var cards = await _cardService.AutocompleteAsync(q);
            return Ok(cards);
        }
    }
}
