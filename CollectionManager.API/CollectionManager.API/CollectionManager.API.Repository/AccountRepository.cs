using CollectionManager.API.Domain;
using CollectionManager.API.Domain.Context;
using CollectionManager.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CollectionManager.API.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly CollectionManagerContext _context;

        public AccountRepository(CollectionManagerContext context)
        {
            _context = context;
        }

        public async Task<string> AddAccount(Account account, User user, Key key)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.Keys.AddAsync(key);
                await _context.Accounts.AddAsync(account);
                await _context.SaveChangesAsync();
                return account.AccountId.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                throw;
            }
        }

        public async Task<(string, string)> GetKey(string userName)
        {
            try
            {
                Account account = await _context.Accounts.FirstOrDefaultAsync(a => a.UserName == userName) ?? new Account();
                Key key = await _context.Keys.FirstOrDefaultAsync(k => k.AccountId == account.UserId) ?? new Key();
                ArgumentNullException.ThrowIfNull(key.Salt);
                ArgumentNullException.ThrowIfNull(account.Password);
                return (key.Salt, account.Password);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                throw;
            }
        }

        public async Task<IEnumerable<string>> GetAllUserNames()
        {
            try
            {
                IEnumerable<string> userNames = await _context.Accounts.Select(a => a.UserName ?? "").ToListAsync();
                return userNames;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                throw;
            }
        }
    }
}

