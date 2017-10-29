using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Supplier.Bll;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Bll
{
    public class SupplierEntityPlugin : ISupplierEntityPlugin
    {
        private readonly IRepository _repository;

        public SupplierEntityPlugin(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> OnDelete(Supplier.Entities.Supplier supplier, Messages messages)
        {
            var hasProducts = await _repository.GetQueryable<Entities.Product, int>()
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
