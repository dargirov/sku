using Administration.Bll;
using Infrastructure.Services.Common;
using StructureMap;

namespace Store.Bll
{
    public static class Config
    {
        public static void RegisterServices(Container container)
        {
            container.Configure(x => x.For<IEntityServicePlugin<Administration.Entities.User>>().Add<UserEntityServicePlugin>());
            container.Configure(x => x.For<IConfigOptionProvider>().Add<ConfigOptionProvider>().Named("StoreConfigOptionProvider"));
            container.Configure(x => x.For<IStoreServices>().Use<StoreServices>());
        }
    }
}
