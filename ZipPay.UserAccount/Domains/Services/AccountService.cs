using AutoMapper;
using System;
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
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountService> _logger;

        private readonly Func<UserModel, double, bool> IsUserEligibleForAccountCreation = (user, creditLimit) =>
        {
            if (user == null)
                return false;

            var userCredit = user.MonthlySalary - user.MonthlyExpenses;
            return user != null && userCredit > creditLimit;
        };

        public AccountService(
            IAccountRepository accountRepository,
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<AccountService> logger)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResponseModel> CreateAccount(AccountModel accountModel, double creditLimit)
        {
            ResponseModel responseModel = new ResponseModel();

            if (accountModel == null || accountModel.UserId <= 0)
            {
                responseModel = ResponseFormatter.CreateResponse(false, Constants.EitherUserOrAccountMissing);
                _logger.LogError(Constants.EitherUserOrAccountMissing);
            }

            //Get user
            var user = await _userRepository.GetUserById(accountModel.UserId);
            if (user == null)
            {
                responseModel = ResponseFormatter.CreateResponse(false, Constants.UserNotFound);
                _logger.LogError(Constants.UserNotFound);
            }

            //Account Eligibilty
            var userModel = _mapper.Map<UserModel>(user);
            if (!IsUserEligibleForAccountCreation(userModel, creditLimit))
            {
                responseModel = ResponseFormatter.CreateResponse(false, Constants.UserNotEligible);
                _logger.LogError(Constants.UserNotEligible);
            }

            //Account Creation
            var account = _mapper.Map<Account>(accountModel);
            var created = account != null ? await _accountRepository.CreateAccount(account) : 0;

            if (created > 0)
            {
                ResponseFormatter.CreateResponse(true, Constants.AccountSuccess);
                _logger.LogError(Constants.AccountSuccess);
            }
            else
            {
                ResponseFormatter.CreateResponse(false, Constants.ErrorCreatingAccount);
                _logger.LogError(Constants.ErrorCreatingAccount);
            }
            
            return responseModel;

        }

        public async Task<IEnumerable<AccountModel>> ListAccounts()
        {
            var account = await _accountRepository.GetAll();
            return account != null ? _mapper.Map<IEnumerable<AccountModel>>(account) : null;
        }
    }
}
