using Administration.Bll;
using StructureMap;

namespace Client.Bll
{
    public static class Config
    {
        public static void RegisterServices(Container container)
        {
            container.Configure(x => x.For<IClientServices>().Use<ClientServices>());
            container.Configure(x => x.For<IGlobalSearchResult>().Add<GlobalSearchResult>().Named("ClientGlobalSearchResult"));
        }
    }
}
