using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Multitenancy
{
    public static class Config
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<ITenantProvider, TenantProvider>();
        }
    }
}
