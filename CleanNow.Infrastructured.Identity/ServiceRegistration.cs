using CleanNow.Core.Application.Interfaces.Shared;
using CleanNow.Infrastructured.Identity.Context;
using CleanNow.Infrastructured.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanNow.Infrastructured.Identity
{
    public static class ServiceRegistration
    {
        public static void AddLayerIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            #region IdentityContext
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(options=>options.UseInMemoryDatabase(configuration.GetConnectionString("UseInMemoryDatabase")));
            }
            else
            {
                services.AddDbContext<IdentityContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("cleanEasyIdentity"),
                        m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                });
            }
            #endregion
            #region Services
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication();
            #endregion
        }
    }
}