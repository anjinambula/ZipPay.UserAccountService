using System.Collections.Generic;
using System.Threading.Tasks;
using ZipPay.UserAccountService.Models;

namespace ZipPay.UserAccountService.Domains.Interfaces
{
    public interface IUserService
    {
        Task<ResponseModel> CreateUser(UserModel user);

        Task<IEnumerable<UserModel>> ListUsers();

        Task<UserModel> GetUserById(int id);        
    }
}
