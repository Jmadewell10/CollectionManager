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

namespace CollectionManager.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _config;

        public AccountService(IAccountRepository accountRepository, IConfiguration config)
        {
            _accountRepository = accountRepository;
            _config = config;
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
                return (GenerateToken(), isValid);
            }
            return (String.Empty, false);
        }
        #endregion

        #region private methods
        private string GenerateToken()
        {
            var secret = _config["JWTVariables:Secret"];
            ArgumentNullException.ThrowIfNull(secret);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JWTVariables:Issuer"],
                audience: _config["JWTVariables:Audience"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
    }
}
