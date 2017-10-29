using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Manufacturer.Bll;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Bll
{
    public class ManufacturerEntityPlugin : IManufacturerEntityPlugin
    {
        private readonly IRepository _repository;

        public ManufacturerEntityPlugin(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> OnDelete(Manufacturer.Entities.Manufacturer manufacturer, Messages messages)
        {
            var hasProducts = await _repository.GetQueryable<Entities.Product, int>()
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