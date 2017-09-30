using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supplier.Bll
{
    public interface ISupplierServices
    {
        Task<Entities.Supplier> GetByIdAsync(int id);
        Task<List<Entities.Supplier>> GetListAsync();
        Task<List<Entities.Supplier>> GetListAsync(string name, string mol, string phone, string address, string email, string url);
        Task<int> EditAsync(Entities.Supplier supplier);
    }
}
