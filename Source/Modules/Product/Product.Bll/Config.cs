using Administration.Bll;
using Infrastructure.Services.Common;
using Manufacturer.Bll;
using Store.Bll;
using StructureMap;
using Supplier.Bll;

namespace Product.Bll
{
    public static class Config
    {
        public static void RegisterServices(Container container)
        {
            // entity plugins
            container.Configure(x => x.For<IEntityServicePlugin<Store.Entities.Store>>().Add<StoreEntityServicePlugin>());
            container.Configure(x => x.For<IEntityServicePlugin<Manufacturer.Entities.Manufacturer>>().Add<ManufacturerEntityServicePlugin>());
            container.Configure(x => x.For<IEntityServicePlugin<Supplier.Entities.Supplier>>().Add<SupplierEntityServicePlugin>());
            container.Configure(x => x.For<IEntityServicePlugin<Entities.ProductCategory>>().Add<ProductCategoryEntityServicePlugin>());
            container.Configure(x => x.For<IEntityServicePlugin<Administration.Entities.User>>().Add<UserEntityServicePlugin>());
            container.Configure(x => x.For<IEntityServicePlugin<Entities.Product>>().Add<ProductEntityServicePlugin>());

            container.Configure(x => x.For<INotificationPlugin>().Add<ProductNotificationPlugin>().Named("ProductNotificationPlugin"));

            // services
            container.Configure(x => x.For<IProductServices>().Use<ProductServices>());
            container.Configure(x => x.For<IGlobalSearchResult>().Add<GlobalSearchResult>().Named("ProductGlobalSearchResult"));
            container.Configure(x => x.For<IPriorityServices>().Use<PriorityServices>());
        }
    }
}
