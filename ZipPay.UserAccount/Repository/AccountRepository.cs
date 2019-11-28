using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZipPay.UserAccountService.DbContexts;
using ZipPay.UserAccountService.Entities;
using ZipPay.UserAccountService.Repository.Interfaces;

namespace ZipPay.UserAccountService.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountContext _dbContext;

        public AccountRepository(AccountContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateAccount(Account account)
        {
            if (account == null)
                return 0;

            _dbContext.Set<Account>().Add(account);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _dbContext.Account.ToListAsync();
        }
    }
}
