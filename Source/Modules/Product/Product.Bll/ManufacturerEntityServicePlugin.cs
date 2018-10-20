using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Bll
{
    public class ManufacturerEntityServicePlugin : IEntityServicePlugin<Manufacturer.Entities.Manufacturer>
    {
        private readonly IRepository _repository;

        public ManufacturerEntityServicePlugin(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> OnDelete(Manufacturer.Entities.Manufacturer manufacturer, Messages messages)
        {
            var hasProducts = await _repository.GetQueryable<Entities.Product>()
                .Where(x => x.Manufacturer == manufacturer)
                .AnyAsync();

            if (hasProducts)
            {
                messages.AddError("Cant delete manufacturer. It has products");
            }

            return !hasProducts;
        }

        public async Task<bool> OnSave(Manufacturer.Entities.Manufacturer manufacturer, Messages messages)
        {
            return true;
        }
    }
}