using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using StructureMap;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supplier.Bll
{
    public class SupplierServices : ISupplierServices
    {
        private readonly IContainer _container;
        private readonly IRepository _repository;
        private readonly IEntityServices _entityServices;

        public SupplierServices(IContainer container, IRepository repository, IEntityServices entityServices)
        {
            _container = container;
            _repository = repository;
            _entityServices = entityServices;
        }

        public Task<Entities.Supplier> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync<Entities.Supplier, int>(id);
        }

        public Task<List<Entities.Supplier>> GetListAsync()
        {
            // TODO: check privs?
            return _repository.GetListAsync<Entities.Supplier, int>();
        }

        public Task<(IEnumerable<Entities.Supplier> suppliers, PageData pageData)> GetListAsync(int page, int pageSize, int column, SortDirectionEnum dir, string name, string mol, string phone, string address, string email, string url)
        {
            // TODO: check privs?
            var query = _repository.GetQueryable<Entities.Supplier, int>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            if (!string.IsNullOrWhiteSpace(mol))
            {
                query = query.Where(x => x.Mol.Contains(mol));
            }

            if (!string.IsNullOrWhiteSpace(phone))
            {
                query = query.Where(x => x.Phone.Contains(phone));
            }

            if (!string.IsNullOrWhiteSpace(address))
            {
                query = query.Where(x => x.Phone.Contains(address));
            }

            if (!string.IsNullOrWhiteSpace(address))
            {
                query = query.Where(x => x.Address.Contains(address));
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(x => x.Email.Contains(email));
            }

            if (!string.IsNullOrWhiteSpace(url))
            {
                query = query.Where(x => x.Url.Contains(url));
            }

            return query.ToListWithPageData(page, pageSize);
        }

        public Task<int> EditAsync(Entities.Supplier supplier)
        {
            return _entityServices.SaveAsync<Entities.Supplier, int>(supplier);
        }

        public async Task<bool> DeleteAsync(Entities.Supplier supplier, Messages messages)
        {
            foreach (var plugin in _container.GetAllInstances<ISupplierEntityPlugin>())
            {
                if (!await plugin.OnDelete(supplier, messages))
                {
                    return false;
                }
            }

            var result = await _entityServices.DeleteAsync<Entities.Supplier, int>(supplier);

            return result != 0;
        }
    }
}
