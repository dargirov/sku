using Administration.Bll;
using Microsoft.Extensions.DependencyInjection;
using Product.Presenters.Widgets.LatestProducts;
using Product.Presenters.Widgets.LowQuantityProducts;
using StructureMap;

namespace Product.Presenters
{
    public static class Config
    {
        public static void RegisterPart(IMvcBuilder builder)
        {
            builder.AddApplicationPart(typeof(Config).Assembly);
        }

        public static void RegisterWidgets(Container container)
        {
            container.Configure(x => x.For<IWidget>().Add<LatestProductsWidget>().Named(nameof(LatestProductsWidget)));
            container.Configure(x => x.For<IWidget>().Add<LowQuantityProductsWidget>().Named(nameof(LowQuantityProductsWidget)));
        }
    }
}
