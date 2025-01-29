using CollectionManager.API.Common;
using CollectionManager.API.Models;
using CollectionManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("AddAccount")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> AddAccount([FromBody] NewAccountDto accountDto)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(accountDto);
                var result = await _accountService.AddAccount(accountDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginCredentialsDto loginCredentials)
        {
            var (token, isValid) = await _accountService.Authenticate(loginCredentials);
            if (isValid)
            {
                return Ok(new { Token = token });
            }
            else
            {
                return Unauthorized(new { Message = ErrorConstants.INCORRECT_CREDENTIALS });
            }
        }
    }
}
