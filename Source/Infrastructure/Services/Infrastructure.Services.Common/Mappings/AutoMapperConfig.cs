using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Services.Common.Mappings
{
    public static class AutoMapperConfig
    {
        //public static MapperConfiguration Configuration { get; private set; }
        public static IMapper Mapper { get; private set; }

        public static void Execute(IEnumerable<TypeInfo> definedTypes)
        {
            var types = definedTypes;
            var config = new MapperConfiguration(cfg =>
            {
                LoadStandartMappings(types, cfg);
                LoadReverseMappings(types, cfg);
                LoadCustomMappings(types, cfg);
            });

            Mapper = config.CreateMapper();

            //Mapper.Initialize(cfg =>
            //{
            //    LoadStandartMappings(types, cfg);
            //    LoadReverseMappings(types, cfg);
            //    LoadCustomMappings(types, cfg);
            //});
        }

        private static void LoadStandartMappings(IEnumerable<TypeInfo> types, IMapperConfigurationExpression config)
        {
            var mappings = (from t in types
                            from i in t.GetInterfaces()
                            where i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                                  !t.IsAbstract &&
                                  !t.IsInterface
                            select new
                            {
                                Source = i.GetGenericArguments()[0],
                                Destination = t.AsType()
                            }).ToArray();

            foreach (var map in mappings)
            {
                config.CreateMap(map.Source, map.Destination);
            }
        }

        private static void LoadReverseMappings(IEnumerable<TypeInfo> types, IMapperConfigurationExpression config)
        {
            var mappings = (from t in types
                            from i in t.GetInterfaces()
                            where i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>) &&
                                  !t.IsAbstract &&
                                  !t.IsInterface
                            select new
                            {
                                Destination = i.GetGenericArguments()[0],
                                Source = t.AsType()
                            }).ToArray();

            foreach (var map in mappings)
            {
                config.CreateMap(map.Source, map.Destination);
            }
        }

        private static void LoadCustomMappings(IEnumerable<TypeInfo> types, IMapperConfigurationExpression mapperConfiguration)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i == typeof(IHaveCustomMappings) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select (IHaveCustomMappings)Activator.CreateInstance(t.AsType())).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(mapperConfiguration);
            }
        }
    }
}
