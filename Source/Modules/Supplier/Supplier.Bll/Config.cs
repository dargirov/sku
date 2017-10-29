using StructureMap;

namespace Supplier.Bll
{
    public static class Config
    {
        public static void RegisterServices(Container container)
        {
            container.Configure(x => x.For<ISupplierServices>().Use<SupplierServices>());
        }
    }
}
