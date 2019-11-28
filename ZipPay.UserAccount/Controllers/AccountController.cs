using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ZipPay.UserAccountService.Domains.Interfaces;
using ZipPay.UserAccountService.Infrastructure;
using ZipPay.UserAccountService.Models;

namespace ZipPay.UserAccountService.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route(ApiRoutes.accountRootRoute)]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _config;

        public AccountController(IAccountService accountService,
               IConfiguration configuration)
        {
            _accountService = accountService;
            _config = configuration;
        }


        [HttpPost]
        [Route(ApiRoutes.createAccountRoute)]
        public async Task<IActionResult> CreateAccount(AccountModel account)
        {            
            var result = await _accountService.CreateAccount(account, Convert.ToDouble(_config["AllowedCreditLimit"]));
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var accounts = await _accountService.ListAccounts();
            if (accounts == null)
                return NotFound();

            return Ok(accounts);
        }

    }
}