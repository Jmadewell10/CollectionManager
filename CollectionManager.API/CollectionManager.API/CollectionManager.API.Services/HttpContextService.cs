using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CollectionManager.API.Domain;
using CollectionManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class HttpContextService : IHttpContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _config;

    public HttpContextService(IHttpContextAccessor httpContextAccessor, IConfiguration config)
    {
        _httpContextAccessor = httpContextAccessor;
        _config = config;
    }

    public Guid GetAccountId()
    {
        var claim = _httpContextAccessor.HttpContext?.User.FindFirst("accountId")?.Value;

        if (string.IsNullOrEmpty(claim) || !Guid.TryParse(claim, out var accountId))
            throw new UnauthorizedAccessException("Account ID not found in token.");

        return accountId;
    }

    public async Task<string> GenerateToken(Account account)
    {
        var secret = _config["JWTVariables:Secret"];
        ArgumentNullException.ThrowIfNull(secret);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
            {
                new Claim("accountId", account.AccountId.ToString()),
                new Claim("userName", account.UserName ?? string.Empty)
            };

        var token = new JwtSecurityToken(
            issuer: _config["JWTVariables:Issuer"],
            audience: _config["JWTVariables:Audience"],
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds,
            claims: claims
        );

        await Task.CompletedTask;

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}