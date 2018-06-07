using Infrastructure.Data.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manufacturer.Bll
{
    public interface IManufacturerServices
    {
        Task<Entities.Manufacturer> GetByIdAsync(int id);
        Task<List<Entities.Manufacturer>> GetListAsync();
        Task<(IEnumerable<Entities.Manufacturer> manufacturers, PageData pageData)> GetListAsync(int page, int pageSize, int column, SortDirectionEnum dir, string name, int? countryId, string email);
        Task<bool> EditAsync(Entities.Manufacturer manufacturer, Messages messages);
        Task<bool> DeleteAsync(Entities.Manufacturer manufacturer, Messages messages);
    }
}
