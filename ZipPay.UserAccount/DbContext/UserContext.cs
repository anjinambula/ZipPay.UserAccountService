using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZipPay.UserAccountService.Entities;
using ZipPay.UserAccountService.Models;

namespace ZipPay.UserAccountService.DbContexts
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        public DbSet<ZipPay.UserAccountService.Models.UserModel> UserModel { get; set; }
    }
}
