using Microsoft.Extensions.DependencyInjection;

namespace Supplier.Bll
{
    public static class Config
    {
        public static void RegisterServices(IServiceCollection service)
        {
            service.AddTransient<ISupplierServices, SupplierServices>();
        }
    }
}
