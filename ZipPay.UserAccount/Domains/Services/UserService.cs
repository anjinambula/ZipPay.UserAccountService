using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ZipPay.UserAccountService.Domains.Interfaces;
using ZipPay.UserAccountService.Entities;
using ZipPay.UserAccountService.Infrastructure;
using ZipPay.UserAccountService.Models;
using ZipPay.UserAccountService.Repository.Interfaces;

namespace ZipPay.UserAccountService.Domains.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ResponseModel> CreateUser(UserModel user)
        {
            ResponseModel responseModel = new ResponseModel();
            if (user == null)
            {
                responseModel = ResponseFormatter.CreateResponse(false, Constants.UserInformationMissing);
                _logger.LogError(Constants.UserInformationMissing);
            }
            var userExists = await _userRepository.IsEmailAddressAlreadyExists(user.EmailAddress);
            if (userExists)
            {
                responseModel =  ResponseFormatter.CreateResponse(false, Constants.EmailAddressAlreadyExists);
                _logger.LogError(Constants.EmailAddressAlreadyExists);
            }

            var userEntity = _mapper.Map<User>(user);
            var created = userEntity != null ? await _userRepository.CreateUser(userEntity) : 0;

            if (created > 0)
            {   
                responseModel = ResponseFormatter.CreateResponse(true, Constants.UserSuccess);
                _logger.LogInformation(Constants.UserSuccess);
            }
            else
            {   
                responseModel = ResponseFormatter.CreateResponse(false, Constants.ErrorCreatingUser);
                _logger.LogError(Constants.ErrorCreatingUser);
            }

            return responseModel;
        }

        public async Task<IEnumerable<UserModel>> ListUsers()
        {
            var users = await _userRepository.GetAll();
            return users != null ? _mapper.Map<IEnumerable<UserModel>>(users) : null;
        }

        public async Task<UserModel> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);                        
            return user != null ? _mapper.Map<UserModel>(user) : null;
        }
    }
}
