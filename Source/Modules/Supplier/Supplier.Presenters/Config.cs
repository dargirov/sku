using Microsoft.Extensions.DependencyInjection;

namespace Supplier.Presenters
{
    public static class Config
    {
        public static void RegisterPart(IMvcBuilder builder)
        {
            builder.AddApplicationPart(typeof(Config).Assembly);
        }
    }
}
