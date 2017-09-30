using Infrastructure.Data.Common;
using System.Threading.Tasks;

namespace Infrastructure.Services.Common
{
    public interface IEntityServices
    {
        Task<int> SaveAsync<TEntity, T>(TEntity entity) where TEntity : BaseTenantEntity<T>;
        Task<int> DeleteAsync<TEntity, T>(TEntity entity) where TEntity : BaseTenantEntity<T>;
    }
}
