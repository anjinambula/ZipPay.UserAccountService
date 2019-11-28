using AutoMapper;
using ZipPay.UserAccountService.Entities;
using ZipPay.UserAccountService.Models;

namespace ZipPay.UserAccountService.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>();               
            CreateMap<UserModel, User>();

            CreateMap<Account, AccountModel>();
            CreateMap<AccountModel, Account>();
        }
    }
}
