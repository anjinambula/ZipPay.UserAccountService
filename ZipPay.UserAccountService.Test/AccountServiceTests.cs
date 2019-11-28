using AutoFixture;
using AutoMapper;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.Logging;
using ZipPay.UserAccountService.Domains.Interfaces;
using ZipPay.UserAccountService.Domains.Services;
using ZipPay.UserAccountService.Entities;
using ZipPay.UserAccountService.Infrastructure;
using ZipPay.UserAccountService.Models;
using ZipPay.UserAccountService.Repository.Interfaces;

namespace ZipPay.UserAccountService.Tests
{
    public class AccountServiceTests
    {
        private IAccountRepository _accountRepository;
        private IUserRepository _userRepository;
        private Fixture _fixture;
        private IMapper _mapper;
        private IAccountService _sut;
        private readonly ILogger<AccountService> _logger;

        public AccountServiceTests()
        {
            _fixture = new Fixture();
            _userRepository = Substitute.For<IUserRepository>();
            _accountRepository = Substitute.For<IAccountRepository>();
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mockMapper.CreateMapper();
            _sut = new AccountService(_accountRepository, _userRepository, _mapper, _logger);
        }

        [Fact]
        public void Can_GetAll_Be_Called()
        {
            _accountRepository.GetAll().ReturnsForAnyArgs(new List<Account>());
            var actual = _sut.ListAccounts();
            _accountRepository.ReceivedWithAnyArgs().GetAll();
        }

        [Fact]
        public void Can_CreateAccount_Not_Be_called_When_User_Not_Found()
        {
            var account = _fixture.Build<Account>().With(x => x.UserId, 2).Create();
            var accountModel = _fixture.Build<AccountModel>().With(x => x.UserId, 0).Create();
            var actual = _sut.CreateAccount(accountModel, 1000);
            _accountRepository.DidNotReceiveWithAnyArgs().CreateAccount(account);
        }

        [Fact]
        public void Can_CreateAccount_Be_called_When_User_Found_With_Valid_Credit()
        {
            var account = _fixture.Build<Account>().With(x => x.UserId, 2).Create();
            var user = _fixture.Build<User>().With(x => x.MonthlySalary, 5000).With(y => y.MonthlyExpenses, 2000).Create();
            _userRepository.GetUserById(2).ReturnsForAnyArgs(user);

            var accountModel = _fixture.Build<AccountModel>().With(x => x.UserId, 2).With(t => t.CurrentBalance, 2000).Create();
            var actual = _sut.CreateAccount(accountModel, 1000);
            _accountRepository.ReceivedWithAnyArgs().CreateAccount(account);
        }

        [Fact]
        public void Can_CreateAccount_Not_Be_called_When_User_Found_With_InValid_Credit()
        {
            var account = _fixture.Build<Account>().With(x => x.UserId, 2).Create();
            var user = _fixture.Build<User>().With(x => x.MonthlySalary, 5000).With(y => y.MonthlyExpenses, 4500).Create();
            _userRepository.GetUserById(2).ReturnsForAnyArgs(user);

            var accountModel = _fixture.Build<AccountModel>().With(x => x.UserId, 2).With(t => t.CurrentBalance, 2000).Create();
            var actual = _sut.CreateAccount(accountModel, 1000);
            _accountRepository.DidNotReceiveWithAnyArgs().CreateAccount(account);
        }
    }
}
