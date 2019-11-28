using System.Collections.Generic;
using System.Threading.Tasks;
using ZipPay.UserAccountService.Entities;

namespace ZipPay.UserAccountService.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<int> CreateUser(User user);

        Task<IEnumerable<User>> GetAll();

        Task<User> GetUserById(int id);

        Task<bool> IsEmailAddressAlreadyExists(string email);

    }
}
