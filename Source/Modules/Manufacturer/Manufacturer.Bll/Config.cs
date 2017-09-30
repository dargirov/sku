using Microsoft.Extensions.DependencyInjection;

namespace Manufacturer.Bll
{
    public static class Config
    {
        public static void RegisterServices(IServiceCollection service)
        {
            service.AddTransient<IManufacturerServices, ManufacturerServices>();
        }
    }
}
