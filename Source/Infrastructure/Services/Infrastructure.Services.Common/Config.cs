using Infrastructure.Services.Common.Authorization;
using Microsoft.AspNetCore.Authorization;
using StructureMap;

namespace Infrastructure.Services.Common
{
    public static class Config
    {
        public static void Register(Container container)
        {
            Multitenancy.Config.Register(container);
            container.Configure(x => x.For<IAuthorizationHandler>().Use<LoggedInHandler>());
            container.Configure(x => x.For<IEntityServices>().Use<EntityServices>());
            container.Configure(x => x.For<ICacheServices>().Use<CacheServices>());
        }
    }
}
