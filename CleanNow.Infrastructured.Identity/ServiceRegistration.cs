using CleanNow.Core.Application.Interfaces.Shared;
using CleanNow.Infrastructured.Identity.Context;
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
            if (configuration.GetValue<bool>("UseInMemoryDb"))
            {
                services.AddDbContext<IdentityContext>(options=>options.UseInMemoryDatabase(configuration.GetConnectionString("DbInMemory")));
            }
            else
            {
                services.AddDbContext<IdentityContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("IdentityBase"),
                        m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                });
            }
            #endregion
            #region Services
            #endregion
        }
    }
}