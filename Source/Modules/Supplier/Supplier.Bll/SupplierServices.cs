using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Supplier.Bll
{
    public class SupplierServices : ISupplierServices
    {
        private readonly IRepository _repository;
        private readonly IEntityServices _entityServices;

        public SupplierServices(IRepository repository, IEntityServices entityServices)
        {
            _repository = repository;
            _entityServices = entityServices;
        }

        public Task<Entities.Supplier> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync<Entities.Supplier, int>(id);
        }

        public Task<List<Entities.Supplier>> GetListAsync()
        {
            return GetListAsync(null, null, null, null, null, null);
        }

        public Task<List<Entities.Supplier>> GetListAsync(string name, string mol, string phone, string address, string email, string url)
        {
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

            return query.ToListAsync();
        }

        public Task<int> EditAsync(Entities.Supplier supplier)
        {
            return _entityServices.SaveAsync<Entities.Supplier, int>(supplier);
        }
    }
}
