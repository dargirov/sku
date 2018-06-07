using Infrastructure.Data.Common;
using System.Threading.Tasks;

namespace Infrastructure.Services.Common
{
    public interface IEntityServices
    {
        Task<bool> SaveAsync<TEntity>(TEntity entity, Messages messages) where TEntity : BaseTenantEntity;
        Task<bool> DeleteAsync<TEntity>(TEntity entity, Messages messages) where TEntity : BaseTenantEntity;
        EntityChangeTree CreateEntityChangeTree<TEntity>(TEntity entity) where TEntity : BaseTenantEntity;
    }
}
