using Infrastructure.Services.Common.Mappings;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ModulesCommon
{
    public static class Config
    {
        public static void SetupModules(Container container, IMvcBuilder builder)
        {
            RegisterParts(builder);
            RegisterServices(container);
            RegisterWidgets(container);
            RegisterMappings();
            //RegisterViewLocations(services);
        }

        private static void RegisterMappings()
        {
            var types = new List<Type>()
            {
                typeof(Administration.Presenters.Config),
                typeof(Store.Presenters.Config),
                typeof(Supplier.Presenters.Config),
                typeof(Manufacturer.Presenters.Config),
                typeof(Client.Presenters.Config),
                typeof(Product.Presenters.Config),
                typeof(Product.Bll.Config),
                typeof(Request.Presenters.Config)
            };

            AutoMapperConfig.Execute(types.SelectMany(x => x.GetTypeInfo().Assembly.DefinedTypes).ToList());
        }

        //private static void RegisterViewLocations(IServiceCollection services)
        //{
        //    var locations = new List<string>();

        //    locations.Add(Store.Presenters.Config.RegisterViewLocation());
        //    locations.Add(Administration.Presenters.Config.RegisterViewLocation());

        //    services.Configure<RazorViewEngineOptions>(options => 
        //    {
        //        options.ViewLocationExpanders.Add(new ViewLocationExpander(locations));
        //    });
        //}

        private static void RegisterServices(Container container)
        {
            Administration.Bll.Config.RegisterServices(container);
            Store.Bll.Config.RegisterServices(container);
            Supplier.Bll.Config.RegisterServices(container);
            Manufacturer.Bll.Config.RegisterServices(container);
            Client.Bll.Config.RegisterServices(container);
            Product.Bll.Config.RegisterServices(container);
            Request.Bll.Config.RegisterServices(container);
        }

        private static void RegisterParts(IMvcBuilder builder)
        {
            Administration.Presenters.Config.RegisterPart(builder);
            Store.Presenters.Config.RegisterPart(builder);
            Supplier.Presenters.Config.RegisterPart(builder);
            Manufacturer.Presenters.Config.RegisterPart(builder);
            Client.Presenters.Config.RegisterPart(builder);
            Product.Presenters.Config.RegisterPart(builder);
            Request.Presenters.Config.RegisterPart(builder);
        }

        private static void RegisterWidgets(Container container)
        {
            Administration.Presenters.Config.RegisterWidgets(container);
            Product.Presenters.Config.RegisterWidgets(container);
            Request.Presenters.Config.RegisterWidgets(container);
        }
    }
}
