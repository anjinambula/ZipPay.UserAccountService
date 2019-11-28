using System.Collections.Generic;
using System.Threading.Tasks;
using ZipPay.UserAccountService.Entities;

namespace ZipPay.UserAccountService.Repository.Interfaces
{
    public interface IAccountRepository
    {

        Task<int> CreateAccount(Account account);

        //Task<IEnumerable<Account>> GetByUserId(int userId);

        Task<IEnumerable<Account>> GetAll();

    }
}
