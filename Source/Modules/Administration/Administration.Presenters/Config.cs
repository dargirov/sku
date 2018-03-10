using Administration.Bll;
using Administration.Presenters.Widgets.Todos;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;

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

        public static void RegisterWidgets(Container container)
        {
            container.Configure(x => x.For<IWidget>().Add<TodosWidget>().Named(nameof(TodosWidget)));
        }
    }
}
