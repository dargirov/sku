using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Bll
{
    public class StoreServices : IStoreServices
    {
        private readonly IRepository _repository;
        private readonly IEntityServices _entityServices;

        public StoreServices(IRepository repository, IEntityServices entityServices)
        {
            _repository = repository;
            _entityServices = entityServices;
        }

        public Task<Entities.Store> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync<Entities.Store, int>(id);
        }

        public Task<List<Entities.Store>> GetListAsync()
        {
            return GetListAsync(null, null, null);
        }

        public Task<List<Entities.Store>> GetListAsync(string name, int? cityId, string address)
        {
            var query = _repository.GetQueryable<Entities.Store, int>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(s => s.Name.Contains(name));
            }

            if (cityId.HasValue && cityId.Value > 0)
            {
                query = query.Where(s => s.CityId == cityId.Value);
            }

            if (!string.IsNullOrWhiteSpace(address))
            {
                query = query.Where(s => s.Address.Contains(address));
            }

            return query.ToListAsync();
        }

        public Task<List<Administration.Entities.City>> GetCityListAsync()
        {
            var s = _repository.GetQueryable<Entities.Store, int>()
                .Select(x => x.CityId);

            return _repository.GetQueryable<Administration.Entities.City, int>()
                .Where(x => s.Contains(x.Id))
                .Distinct()
                .ToListAsync();
        }

        public Task<int> EditAsync(Entities.Store store)
        {
            return _entityServices.SaveAsync<Entities.Store, int>(store);
        }
    }
}
