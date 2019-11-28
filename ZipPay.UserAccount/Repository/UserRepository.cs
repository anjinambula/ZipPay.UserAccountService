using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZipPay.UserAccountService.DbContexts;
using ZipPay.UserAccountService.Entities;

namespace ZipPay.UserAccountService.Repository.Interfaces
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _dbContext;

        public UserRepository(UserContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateUser(User user)
        {
            if (user == null)
                return 0;

            _dbContext.Set<User>().Add(user);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _dbContext.User.ToListAsync();
        }

        public async Task<bool> IsEmailAddressAlreadyExists(string email)
        {
            return await _dbContext.User.Where(t => t.EmailAddress.Trim() == email.Trim()).AnyAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _dbContext.User.Where(t => t.Id == id).FirstOrDefaultAsync();
        }
    }
}
