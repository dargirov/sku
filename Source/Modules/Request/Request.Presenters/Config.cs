using Microsoft.Extensions.DependencyInjection;
using StructureMap;

namespace Request.Presenters
{
    public static class Config
    {
        public static void RegisterPart(IMvcBuilder builder)
        {
            builder.AddApplicationPart(typeof(Config).Assembly);
        }

        public static void RegisterWidgets(Container container)
        {
        }
    }
}
