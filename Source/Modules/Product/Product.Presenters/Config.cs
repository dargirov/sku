using Microsoft.Extensions.DependencyInjection;

namespace Product.Presenters
{
    public static class Config
    {
        public static void RegisterPart(IMvcBuilder builder)
        {
            builder.AddApplicationPart(typeof(Config).Assembly);
        }
    }
}
