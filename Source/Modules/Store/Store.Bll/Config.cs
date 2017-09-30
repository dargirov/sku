using Microsoft.Extensions.DependencyInjection;

namespace Store.Bll
{
    public static class Config
    {
        public static void RegisterServices(IServiceCollection service)
        {
            service.AddTransient<IStoreServices, StoreServices>();
        }
    }
}
