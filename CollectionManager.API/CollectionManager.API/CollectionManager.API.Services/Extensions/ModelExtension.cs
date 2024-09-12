using CollectionManager.API.Domain;
using CollectionManager.API.Models;

namespace CollectionManager.API.Services.Extensions
{
    public static class ModelExtension
    {
        public static (Account, User, Key) CreateNewAccountObjectsFromDto(this NewAccountDto accountDto)
        {
            accountDto.ValidateNewAccountDto();

            var (hash, salt) = PasswordHashExtension.HashPassword(accountDto.Login?.Password ?? "");

            Account account = new Account()
            {
                AccountId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = accountDto.Login?.Username,
                Password = hash
            };

            User user = new User()
            {
                UserId = account.UserId,
                AccountId = account.AccountId,
                FirstName = accountDto.FirstName,
                LastName = accountDto.LastName,
                Email = accountDto.Email

            };

            Key key = new Key()
            {
                KeyId = Guid.NewGuid(),
                AccountId = user.AccountId,
                Salt = salt
            };

            account.UserId = user.UserId;
            user.AccountId = account.AccountId;
            user.Account = account;
            account.KeyId = key.KeyId;

            return (account, user, key);
        }
    }
}
