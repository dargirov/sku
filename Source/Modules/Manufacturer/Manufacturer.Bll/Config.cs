using Administration.Bll;
using StructureMap;

namespace Manufacturer.Bll
{
    public static class Config
    {
        public static void RegisterServices(Container container)
        {
            container.Configure(x => x.For<IManufacturerServices>().Use<ManufacturerServices>());
            container.Configure(x => x.For<IGlobalSearchResult>().Add<GlobalSearchResult>().Named("ManufacturerGlobalSearchResult"));
        }
    }
}
