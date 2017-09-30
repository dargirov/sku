using Infrastructure.Services.Common.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ModulesCommon
{
    public static class Config
    {
        public static void SetupModules(IServiceCollection services, IMvcBuilder builder)
        {
            RegisterParts(builder);
            RegisterServices(services);
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

        private static void RegisterServices(IServiceCollection service)
        {
            Administration.Bll.Config.RegisterServices(service);
            Store.Bll.Config.RegisterServices(service);
            Supplier.Bll.Config.RegisterServices(service);
            Manufacturer.Bll.Config.RegisterServices(service);
            Client.Bll.Config.RegisterServices(service);
            Product.Bll.Config.RegisterServices(service);
        }

        private static void RegisterParts(IMvcBuilder builder)
        {
            Administration.Presenters.Config.RegisterPart(builder);
            Store.Presenters.Config.RegisterPart(builder);
            Supplier.Presenters.Config.RegisterPart(builder);
            Manufacturer.Presenters.Config.RegisterPart(builder);
            Client.Presenters.Config.RegisterPart(builder);
            Product.Presenters.Config.RegisterPart(builder);
        }
    }
}
