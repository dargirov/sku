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
            return _repository.GetByIdAsync<Entities.Manufacturer>(id);
        }

        public Task<List<Entities.Manufacturer>> GetListAsync()
        {
            // TODO: shouldn't I check privileges?
            return _repository.GetListAsync<Entities.Manufacturer>();
        }

        public Task<(IEnumerable<Entities.Manufacturer> manufacturers, PageData pageData)> GetListAsync(int page, int pageSize, int column, SortDirectionEnum dir, string name, int? countryId, string email)
        {
            // TODO: shouldn't I check privileges?
            var query = _repository.GetQueryable<Entities.Manufacturer>();

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

        public Task<bool> EditAsync(Entities.Manufacturer manufacturer, Messages messages)
        {
            return _entityServices.SaveAsync<Entities.Manufacturer>(manufacturer, messages);
        }

        public async Task<bool> DeleteAsync(Entities.Manufacturer manufacturer, Messages messages)
        {
            return await _entityServices.DeleteAsync<Entities.Manufacturer>(manufacturer, messages);
        }
    }
}
