using AutoMapper;

namespace Infrastructure.Services.Common.Mappings
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
