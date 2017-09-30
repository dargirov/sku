using Microsoft.Extensions.DependencyInjection;

namespace Client.Bll
{
    public static class Config
    {
        public static void RegisterServices(IServiceCollection service)
        {
            service.AddTransient<IClientServices, ClientServices>();
        }
    }
}
