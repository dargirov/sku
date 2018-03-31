using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using StructureMap;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manufacturer.Bll
{
    public class ManufacturerServices : IManufacturerServices
    {
        private readonly IContainer _container;
        private readonly IRepository _repository;
        private readonly IEntityServices _entityServices;

        public ManufacturerServices(IContainer container, IRepository repository, IEntityServices entityServices)
        {
            _container = container;
            _repository = repository;
            _entityServices = entityServices;
        }

        public Task<Entities.Manufacturer> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync<Entities.Manufacturer, int>(id);
        }

        public Task<List<Entities.Manufacturer>> GetListAsync()
        {
            // TODO: shouldn't I check privileges?
            return _repository.GetListAsync<Entities.Manufacturer, int>();
        }

        public Task<(IEnumerable<Entities.Manufacturer> manufacturers, PageData pageData)> GetListAsync(int page, int pageSize, int column, SortDirectionEnum dir, string name, int? countryId, string email)
        {
            // TODO: shouldn't I check privileges?
            var query = _repository.GetQueryable<Entities.Manufacturer, int>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            if (countryId.HasValue && countryId.Value > 0)
            {
                query = query.Where(x => x.CountryId == countryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(x => x.Email.Contains(email));
            }

            return query.ToListWithPageData(page, pageSize);
        }

        public Task<int> EditAsync(Entities.Manufacturer manufacturer)
        {
            return _entityServices.SaveAsync<Entities.Manufacturer, int>(manufacturer);
        }

        public async Task<bool> DeleteAsync(Entities.Manufacturer manufacturer, Messages messages)
        {
            foreach (var plugin in _container.GetAllInstances<IManufacturerEntityPlugin>())
            {
                if (!await plugin.OnDelete(manufacturer, messages))
                {
                    return false;
                }
            }

            var result = await _entityServices.DeleteAsync<Entities.Manufacturer, int>(manufacturer);

            return result != 0;
        }
    }
}
