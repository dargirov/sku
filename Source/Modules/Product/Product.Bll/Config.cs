using Microsoft.Extensions.DependencyInjection;

namespace Product.Bll
{
    public static class Config
    {
        public static void RegisterServices(IServiceCollection service)
        {
            service.AddTransient<IProductServices, ProductServices>();
        }
    }
}
