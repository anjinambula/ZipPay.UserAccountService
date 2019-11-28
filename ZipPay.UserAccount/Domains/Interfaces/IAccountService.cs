using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZipPay.UserAccountService.Models;

namespace ZipPay.UserAccountService.Domains.Interfaces
{
    public interface IAccountService
    {        
        Task<ResponseModel> CreateAccount(AccountModel user, double creditLimit);

        Task<IEnumerable<AccountModel>> ListAccounts();
    }
}
