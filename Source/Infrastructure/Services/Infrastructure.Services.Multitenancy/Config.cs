using StructureMap;

namespace Infrastructure.Services.Multitenancy
{
    public static class Config
    {
        public static void Register(Container container)
        {
            container.Configure(x => x.For<ITenantProvider>().Use<TenantProvider>());
        }
    }
}
