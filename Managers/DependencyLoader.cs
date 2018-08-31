using Microsoft.Extensions.DependencyInjection;
using System;

namespace ParityService.Managers
{
    public static class DependencyLoader
    {
        public static void AddManagers(this IServiceCollection services)
        {
            services.AddScoped<ServiceLinkManager>();
            services.AddScoped<CredentialsManager>();
        }
    }
}
