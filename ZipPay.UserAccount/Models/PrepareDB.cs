using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZipPay.UserAccountService.DbContexts;

namespace ZipPay.UserAccountService.Models
{
    public static class PrepareDB
    {
        public static void PrepareSchema(IApplicationBuilder app)
        {
            using (var servicescope = app.ApplicationServices.CreateScope())
            {
                RunMigrations(servicescope.ServiceProvider.GetService<UserContext>());
            }
        }

        public static void RunMigrations(UserContext dbContext)
        {
            System.Console.WriteLine("Applying Migrations...");
            dbContext.Database.Migrate();
        }
    }
}