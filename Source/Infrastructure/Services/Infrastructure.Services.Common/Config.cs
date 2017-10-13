using Infrastructure.Services.Common.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.Common
{
    public static class Config
    {
        public static void Register(IServiceCollection services)
        {
            Multitenancy.Config.Register(services);
            services.AddSingleton<IAuthorizationHandler, LoggedInHandler>();
            services.AddTransient<IEntityServices, EntityServices>();
            services.AddTransient<ICacheServices, CacheServices>();
        }
    }
}
