using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Database.DbConfig
{
    public static class Config
    {
        public static void Register(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly(typeof(Config).Name)));
        }
    }
}
