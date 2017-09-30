using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manufacturer.Entities;

namespace Manufacturer.Bll
{
    public class ManufacturerServices : IManufacturerServices
    {
        private readonly IRepository _repository;
        private readonly IEntityServices _entityServices;

        public ManufacturerServices(IRepository repository, IEntityServices entityServices)
        {
            _repository = repository;
            _entityServices = entityServices;
        }

        public Task<Entities.Manufacturer> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync<Entities.Manufacturer, int>(id);
        }

        public Task<List<Entities.Manufacturer>> GetListAsync()
        {
            return GetListAsync(null, null, null);
        }

        public Task<List<Entities.Manufacturer>> GetListAsync(string name, int? countryId, string email)
        {
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

            return query.ToListAsync();
        }

        public Task<int> EditAsync(Entities.Manufacturer manufacturer)
        {
            return _entityServices.SaveAsync<Entities.Manufacturer, int>(manufacturer);
        }
    }
}
