using Administration.Bll;
using Administration.Bll.Dtos;
using Administration.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Bll
{
    public class ConfigOptionProvider : IConfigOptionProvider
    {
        private readonly IStoreServices _storeServices;

        public ConfigOptionProvider(IStoreServices storeServices)
        {
            _storeServices = storeServices;
        }

        public async Task<IList<ConfigOptionDto>> GetApiOptions()
        {
            return (await _storeServices.GetListAsync())
                .Select(x => new ConfigOptionDto()
                {
                    EntityId = x.Id,
                    Entity = nameof(Entities.Store),
                    Value = "False",
                    DisplayValue = x.Name,
                    Type = ConfigOptionTypeEnum.Bool,
                    Category = ConfigOptionCategoryEnum.Api
                })
                .ToList();
        }
    }
}
