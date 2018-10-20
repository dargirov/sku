using Infrastructure.Data.Common;
using System.Threading.Tasks;

namespace Infrastructure.Services.Common
{
    public interface IEntityServicePlugin<T> where T : BaseTenantEntity
    {
        Task<bool> OnSave(T entity, Messages messages);
        Task<bool> OnDelete(T entity, Messages messages);
    }
}
