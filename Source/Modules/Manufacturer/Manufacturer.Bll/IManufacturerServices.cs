using Infrastructure.Data.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manufacturer.Bll
{
    public interface IManufacturerServices
    {
        Task<Entities.Manufacturer> GetByIdAsync(int id);
        Task<List<Entities.Manufacturer>> GetListAsync();
        Task<List<Entities.Manufacturer>> GetListAsync(string name, int? countryId, string email);
        Task<int> EditAsync(Entities.Manufacturer manufacturer);
        Task<bool> DeleteAsync(Entities.Manufacturer manufacturer, Messages messages);
    }
}
