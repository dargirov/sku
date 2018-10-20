using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Bll
{
    public class StoreEntityServicePlugin : IEntityServicePlugin<Store.Entities.Store>
    {
        private readonly IRepository _repository;

        public StoreEntityServicePlugin(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> OnDelete(Store.Entities.Store store, Messages messages)
        {
            var hasProducts = await _repository.GetQueryable<Entities.ProductStock>()
                .Where(x => x.Store == store)
                .AnyAsync();

            if (hasProducts)
            {
                messages.AddError("Cant delete store. It has products");
            }

            return !hasProducts;
        }

        public async Task<bool> OnSave(Store.Entities.Store store, Messages messages)
        {
            return true;
        }
    }
}