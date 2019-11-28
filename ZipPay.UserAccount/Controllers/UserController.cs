using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZipPay.UserAccountService.Domains.Interfaces;
using ZipPay.UserAccountService.Infrastructure;
using ZipPay.UserAccountService.Models;

namespace ZipPay.UserAccountService.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route(ApiRoutes.usersRootRoute)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost]
        [Route(ApiRoutes.createUserRoute)]
        public async Task<IActionResult> CreateUser(UserModel user)
        {
            if (user == null)
                return BadRequest();

            var result = await _userService.CreateUser(user);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.ListUsers();
            if (users == null)
                return NotFound();

            return Ok(users);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest();

            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound();

            return Ok(user);

        }
    }
}