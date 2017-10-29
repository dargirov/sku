using Infrastructure.Data.Common;
using System.Threading.Tasks;

namespace Supplier.Bll
{
    public interface ISupplierEntityPlugin
    {
        Task<bool> OnSave(Entities.Supplier supplier, Messages messages);
        Task<bool> OnDelete(Entities.Supplier supplier, Messages messages);
    }
}
