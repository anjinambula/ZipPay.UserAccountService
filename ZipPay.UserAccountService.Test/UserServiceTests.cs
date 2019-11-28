using AutoFixture;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System.Collections.Generic;
using Xunit;
using Microsoft.Extensions.Logging;
using ZipPay.UserAccountService.Domains.Interfaces;
using ZipPay.UserAccountService.Domains.Services;
using ZipPay.UserAccountService.Entities;
using ZipPay.UserAccountService.Models;
using ZipPay.UserAccountService.Repository.Interfaces;
using ZipPay.UserAccountService.Infrastructure;

namespace ZipPay.UserAccountService.Tests
{
    public class UserServiceTests
    {
        private IUserRepository _userRepository;
        private Fixture _fixture;
        private IMapper _mapper;
        private IUserService _sut;
        private readonly ILogger<UserService> _logger;
        public UserServiceTests()
        {
            _fixture = new Fixture();
            _userRepository = Substitute.For<IUserRepository>();   
            
            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mockMapper.CreateMapper();
            _sut = new UserService(_userRepository, _mapper, _logger);
        }

        [Fact]
        public void Can_GetAll_Be_Called()
        {
            _userRepository.GetAll().ReturnsForAnyArgs(new List<User>());
            var actual = _sut.ListUsers();
            _userRepository.ReceivedWithAnyArgs().GetAll();
        }

        [Fact]
        public void Can_GetUserById_Be_Called()
        {
            _userRepository.GetUserById(1).ReturnsForAnyArgs(new User());
            var actual = _sut.GetUserById(1);
            _userRepository.ReceivedWithAnyArgs().GetUserById(1);
        }


        [Fact]
        public void Can_CreateUser_Be_called_When_User_Not_Exist()
        {
            var user = _fixture.Build<User>().With(x => x.EmailAddress, "TestUser@test.com").Create();            
            _userRepository.CreateUser(user).ReturnsForAnyArgs(1);
            _userRepository.IsEmailAddressAlreadyExists(string.Empty).ReturnsForAnyArgs(false);

            var userModel = _fixture.Build<UserModel>().With(x => x.EmailAddress, "TestUser@gtest.com").Create();
            var actual = _sut.CreateUser(userModel);
            _userRepository.ReceivedWithAnyArgs().CreateUser(user);
            _userRepository.ReceivedWithAnyArgs().IsEmailAddressAlreadyExists(string.Empty);
        }

        [Fact]
        public void Can_CreateUser_Not_Be_called_When_User_Exist()
        {
            var user = _fixture.Build<User>().With(x => x.EmailAddress, "TestUser@test.com").Create();
            _userRepository.CreateUser(user).ReturnsForAnyArgs(1);
            _userRepository.IsEmailAddressAlreadyExists(string.Empty).ReturnsForAnyArgs(true);

            var userModel = _fixture.Build<UserModel>().With(x => x.EmailAddress, "TestUser@test.com").Create();
            var actual = _sut.CreateUser(userModel);
            _userRepository.ReceivedWithAnyArgs().IsEmailAddressAlreadyExists(string.Empty);
            _userRepository.DidNotReceiveWithAnyArgs().CreateUser(user);
        }

        [Fact]
        public void Can_GetAll_Return_Result()
        {
            var users = _fixture.Build<User>().With(x => x.MonthlySalary, 1000).CreateMany(3);
            _userRepository.GetAll().Returns(users);

            var actual = _sut.ListUsers();
            actual.Should().NotBeNull();
        }

        [Fact]
        public void Can_GetUserById_Return_Result()
        {
            var user = _fixture.Build<User>().With(x => x.Id, 20).Create();
            _userRepository.GetUserById(20).ReturnsForAnyArgs(user);

            var actual = _sut.GetUserById(20);
            actual.Should().NotBeNull();
        }
    }
}
