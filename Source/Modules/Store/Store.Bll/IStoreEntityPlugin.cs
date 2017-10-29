using Infrastructure.Data.Common;
using System.Threading.Tasks;

namespace Store.Bll
{
    public interface IStoreEntityPlugin
    {
        Task<bool> OnSave(Entities.Store store, Messages messages);
        Task<bool> OnDelete(Entities.Store store, Messages messages);
    }
}
