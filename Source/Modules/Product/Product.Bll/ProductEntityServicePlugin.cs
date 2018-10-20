using Infrastructure.Data.Common;
using Infrastructure.Services.Common;
using System.Threading.Tasks;

namespace Product.Bll
{
    public class ProductEntityServicePlugin : IEntityServicePlugin<Entities.Product>
    {
        public async Task<bool> OnDelete(Entities.Product product, Messages messages)
        {
            return true;
        }

        public async Task<bool> OnSave(Entities.Product product, Messages messages)
        {
            return true;
        }
    }
}
