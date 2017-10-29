using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Store.Bll;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Bll
{
    public class StoreEntityPlugin : IStoreEntityPlugin
    {
        private readonly IRepository _repository;

        public StoreEntityPlugin(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> OnDelete(Store.Entities.Store store, Messages messages)
        {
            var hasProducts = await _repository.GetQueryable<Entities.ProductStock, int>()
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