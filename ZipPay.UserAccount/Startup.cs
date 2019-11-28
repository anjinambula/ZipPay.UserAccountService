using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ZipPay.UserAccountService.DbContexts;
using ZipPay.UserAccountService.Domains.Interfaces;
using ZipPay.UserAccountService.Infrastructure;
using ZipPay.UserAccountService.Repository;
using ZipPay.UserAccountService.Repository.Interfaces;
using ZipPay.UserAccountService.Domains.Services;
using ZipPay.UserAccountService.Models;
//using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;

namespace ZipPay.UserAccountService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            
            //Swagger
            services.AddDbContextPool<UserContext>(options => options.UseSqlServer($"Server=(localdb)\\MSSQLLocalDB;Database=Account;Trusted_Connection=True;"));
            services.AddDbContextPool<AccountContext>(options => options.UseSqlServer($"Server=(localdb)\\MSSQLLocalDB;Database=Account;Trusted_Connection=True;"));

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        
    }
}
