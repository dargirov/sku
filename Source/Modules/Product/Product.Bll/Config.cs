using Administration.Bll;
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
            container.Configure(x => x.For<IStoreEntityPlugin>().Add<StoreEntityPlugin>().Named("ProductStoreEntityPlugin"));
            container.Configure(x => x.For<IManufacturerEntityPlugin>().Add<ManufacturerEntityPlugin>().Named("ProductManufacturerEntityPlugin"));
            container.Configure(x => x.For<ISupplierEntityPlugin>().Add<SupplierEntityPlugin>().Named("ProductSupplierEntityPlugin"));
            container.Configure(x => x.For<IProductCategoryEntityPlugin>().Add<ProductCategoryEntityPlugin>().Named("ProductProductCategoryEntityPlugin"));

            container.Configure(x => x.For<INotificationPlugin>().Add<ProductNotificationPlugin>().Named("ProductNotificationPlugin"));

            // services
            container.Configure(x => x.For<IProductServices>().Use<ProductServices>());
            container.Configure(x => x.For<IGlobalSearchResult>().Add<GlobalSearchResult>().Named("ProductGlobalSearchResult"));
        }
    }
}
