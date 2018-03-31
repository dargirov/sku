using Infrastructure.Data.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supplier.Bll
{
    public interface ISupplierServices
    {
        Task<Entities.Supplier> GetByIdAsync(int id);
        Task<List<Entities.Supplier>> GetListAsync();
        Task<(IEnumerable<Entities.Supplier> suppliers, PageData pageData)> GetListAsync(int page, int pageSize, int column, SortDirectionEnum dir, string name, string mol, string phone, string address, string email, string url);
        Task<int> EditAsync(Entities.Supplier supplier);
        Task<bool> DeleteAsync(Entities.Supplier supplier, Messages messages);
    }
}
