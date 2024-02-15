using CleanNow.Core.Application.Interfaces.Shared;
using CleanNow.Core.Domain.Settings;
using CleanNow.Infrastructured.Shared.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanNow.Infrastructured.Shared
{
    public static class ServiceRegistration
    {
        public static void AddLayerShared(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}