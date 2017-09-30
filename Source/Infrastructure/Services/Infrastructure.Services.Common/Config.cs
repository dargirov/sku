using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Common
{
    public static class Config
    {
        public static void Register(IServiceCollection services)
        {
            Multitenancy.Config.Register(services);
            services.AddTransient<IEntityServices, EntityServices>();
        }
    }
}
