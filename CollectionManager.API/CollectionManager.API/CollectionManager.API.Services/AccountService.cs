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
        private readonly IHttpContextService _httpService;

        public AccountService(IAccountRepository accountRepository, IHttpContextService httpService)
        {
            _accountRepository = accountRepository;
            _httpService = httpService;
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
                return (await _httpService.GenerateToken(account), isValid);
            }
            return (String.Empty, false);
        }

        public async Task<string> CheckToken()
        {
            var accountId = _httpService.GetAccountId();

            var account = await _accountRepository.GetAccountById(accountId);
            return await _httpService.GenerateToken(account);
        }

        public async Task<Account> GetAccountFromToken()
        {           
            var accountId = _httpService.GetAccountId();
            return await _accountRepository.GetAccountById(accountId);
        }

        #endregion

    }
}
