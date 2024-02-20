using CleanNow.Core.Application.Interfaces.Repositories;
using CleanNow.Infrastructured.Persistence.Context;
using CleanNow.Infrastructured.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanNow.Infrastructured.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddLayerPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options =>options.UseInMemoryDatabase(configuration.GetConnectionString("UseInMemoryDatabase")));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("cleanEasyConnection"), m =>
                    m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
                });
            }
            #endregion
            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GeneryRepository<>));
            #endregion
        }
    }
}