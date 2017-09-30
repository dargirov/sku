using Microsoft.Extensions.DependencyInjection;

namespace Administration.Presenters
{
    public static class Config
    {
        public static void RegisterPart(IMvcBuilder builder)
        {
            builder.AddApplicationPart(typeof(Config).Assembly);
        }

        //public static string RegisterViewLocation()
        //{
        //    return @"/Views/{1}/{0}.cshtml";
        //}
    }
}
