using System.Runtime.InteropServices;
using CollectionManager.API.Domain;
using CollectionManager.API.Models;
using CollectionManager.API.Services;
using CollectionManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpGet("GetCollections")]
        public async Task<IActionResult> GetCollections(string userId) {
            try
            {
                var result = await _collectionService.GetCollections(userId);
                return Ok(result);   
            }
            catch(Exception e)
            {
                Console.WriteLine($"{e.Message}");
                return StatusCode(500, e);
            }
        }

        [HttpPost("CreateCollection")]
        public async Task<IActionResult> CreateCollection(NewCollectionDto newCollection) 
        {
            try
            {
                var result = await _collectionService.CreateCollection(newCollection);
                return Ok(result);
            }
            catch(Exception e)
            {
                Console.WriteLine($"{e.Message}");
                return StatusCode(500, e);
            }
        }

    }
}