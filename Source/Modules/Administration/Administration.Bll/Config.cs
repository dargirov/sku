using Microsoft.Extensions.DependencyInjection;

namespace Administration.Bll
{
    public static class Config
    {
        public static void RegisterServices(IServiceCollection service)
        {
            service.AddTransient<IUserServices, UserServices>();
            service.AddTransient<ICityServices, CityServices>();
            service.AddTransient<ICountryServices, CountryServices>();
            service.AddTransient<IFileServices, FileServices>();
        }
    }
}
