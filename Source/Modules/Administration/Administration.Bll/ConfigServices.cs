using Administration.Bll.Dtos;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using StructureMap;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public class ConfigServices : IConfigServices
    {
        private readonly IRepository _repository;
        private readonly IContainer _container;
        private readonly IEntityServices _entityServices;

        public ConfigServices(IRepository repository, IContainer container, IEntityServices entityServices)
        {
            _repository = repository;
            _container = container;
            _entityServices = entityServices;
        }

        public async Task<IList<ConfigOptionDto>> GetListAsync(Entities.ConfigOptionCategoryEnum category)
        {
            var result = new List<ConfigOptionDto>();
            result.AddRange(GetAdministrationConfigOptions(category));

            switch (category)
            {
                case Entities.ConfigOptionCategoryEnum.Api:
                    result.AddRange(await GetApiOptions());
                    break;
            }

            var dbOptions = await _repository.GetListAsync<Entities.ConfigOption, int>(x => x.Category == category);

            foreach (var dbOption in dbOptions)
            {
                var option = result.FirstOrDefault(x => x.EntityId == dbOption.EntityId && x.Entity == dbOption.Entity && x.Type == dbOption.Type);
                if (option != null)
                {
                    option.Id = dbOption.Id;
                    option.Value = dbOption.Value;
                }
            }

            return result;
        }

        private IEnumerable<ConfigOptionDto> GetAdministrationConfigOptions(Entities.ConfigOptionCategoryEnum category)
        {
            // TODO: create function to give default config options
            // so I can know which option is an IP addres and so on
            var result = new List<ConfigOptionDto>
            {
                new ConfigOptionDto()
                {
                    Category = Entities.ConfigOptionCategoryEnum.Api,
                    DisplayValue = "Allowed IP Addresses",
                    Entity = nameof(Entities.ConfigOption),
                    EntityId = 1,
                    Type = Entities.ConfigOptionTypeEnum.String,
                    Value = string.Empty
                }
            };

            return result;
        }

        public async Task<int> EditAsync(IEnumerable<ConfigOptionDto> configOptionDtos)
        {
            using (var transaction = await _repository.BeginTransactionAsync())
            {
                foreach (var configOptionDto in configOptionDtos.Where(x => x.Id > 0))
                {
                    var configOption = await _repository.GetByIdAsync<Entities.ConfigOption, int>(configOptionDto.Id);
                    configOption.Value = configOptionDto.Value;
                    await _entityServices.SaveAsync<Entities.ConfigOption, int>(configOption);
                }

                foreach (var configOptionDto in configOptionDtos.Where(x => x.Id == 0))
                {
                    var configOption = new Entities.ConfigOption()
                    {
                        EntityId = configOptionDto.EntityId,
                        Entity = configOptionDto.Entity,
                        Type = configOptionDto.Type,
                        Value = configOptionDto.Value,
                        Category = configOptionDto.Category
                    };

                    await _entityServices.SaveAsync<Entities.ConfigOption, int>(configOption);
                }

                transaction.Commit();
            }

            return 1;
        }

        private async Task<IList<ConfigOptionDto>> GetApiOptions()
        {
            var result = new List<ConfigOptionDto>();

            foreach (var provider in _container.GetAllInstances<IConfigOptionProvider>())
            {
                result.AddRange(await provider.GetApiOptions());
            }

            return result;
        }
    }
}
