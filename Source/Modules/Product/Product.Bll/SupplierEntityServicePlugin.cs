using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Bll
{
    public class SupplierEntityServicePlugin : IEntityServicePlugin<Supplier.Entities.Supplier>
    {
        private readonly IRepository _repository;

        public SupplierEntityServicePlugin(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> OnDelete(Supplier.Entities.Supplier supplier, Messages messages)
        {
            var hasProducts = await _repository.GetQueryable<Entities.Product>()
                .Where(x => x.Supplier == supplier)
                .AnyAsync();

            if (hasProducts)
            {
                messages.AddError("Cant delete supplier. It has products");
            }

            return !hasProducts;
        }

        public async Task<bool> OnSave(Supplier.Entities.Supplier supplier, Messages messages)
        {
            return true;
        }
    }
}
