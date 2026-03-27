using CollectionManager.API.Models;
using CollectionManager.API.Services.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionManager.API.Common;
using CollectionManager.API.Services.Interfaces;
using CollectionManager.API.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using CollectionManager.API.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace CollectionManager.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(IAccountRepository accountRepository, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _accountRepository = accountRepository;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        #region public methods
        public async Task<string> AddAccount(NewAccountDto accountDto)
        {
            IEnumerable<string> users = await _accountRepository.GetAllUserNames();
            ArgumentNullException.ThrowIfNull(accountDto.Login);
            bool isValidLoginCred = accountDto.Login.ValidateNewLoginCred(users);
            if (!isValidLoginCred)
            {
                throw new InvalidOperationException("Invalid login credentials");
            }
            var (account, user, key) = accountDto.CreateNewAccountObjectsFromDto();
            string id = await _accountRepository.AddAccount(account, user, key);
            return id;
        }

        public async Task<(string, bool)> Authenticate(LoginCredentialsDto loginCredentials)
        {
            loginCredentials.ValidateLoginCred();
            var (storedSalt, storedPassword) = await _accountRepository.GetKey(loginCredentials.Username ?? String.Empty);
            bool isValid = PasswordHashExtension.VerifyPassword(loginCredentials.Password ?? String.Empty, storedPassword, storedSalt);
            if (isValid)
            {
                ArgumentNullException.ThrowIfNull(loginCredentials.Username);
                var account = await _accountRepository.GetAccountByUserName(loginCredentials.Username); 
                return (await GenerateToken(account), isValid);
            }
            return (String.Empty, false);
        }

        public async Task<string> CheckToken()
        {
            var claim = _httpContextAccessor.HttpContext?.User.FindFirst("accountId")?.Value;

            if(string.IsNullOrEmpty(claim) || Guid.TryParse(claim, out var accountId)){
                throw new UnauthorizedAccessException("Account ID not found in token.");
            }

            var account = await _accountRepository.GetAccountById(accountId);
            return await GenerateToken(account);
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

        public async Task<Account> GetAccountFromToken()
        {
            var claim = _httpContextAccessor.HttpContext?.User
                .FindFirst("accountId")?.Value;

            if (string.IsNullOrEmpty(claim) || !Guid.TryParse(claim, out var accountId))
                throw new UnauthorizedAccessException("Account ID not found in token.");

            

            return await _accountRepository.GetAccountById(accountId);
        }

        #endregion

    }
}
