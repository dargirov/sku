using Administration.Bll;
using Microsoft.Extensions.DependencyInjection;
using Request.Presenters.Widgets.PendingRequests;
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
            container.Configure(x => x.For<IWidget>().Add<PendingRequestsWidget>().Named(nameof(PendingRequestsWidget)));
        }
    }
}
