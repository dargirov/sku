using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Multitenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Infrastructure.Services.Common
{
    public class EntityServices : IEntityServices
    {
        private readonly IRepository _repository;
        private readonly Guid _tenantId;

        public EntityServices(IRepository repository, ITenantProvider tenantProvider)
        {
            _repository = repository;
            _tenantId = tenantProvider.GetTenantId();
        }

        // TODO: SAVE MEMO
        public Task<int> SaveAsync<TEntity, T>(TEntity entity) where TEntity : BaseTenantEntity<T>
        {
            SaveInternal<TEntity, T>(entity, true);

            return _repository.SaveAsync();
        }

        public Task<int> DeleteAsync<TEntity, T>(TEntity entity) where TEntity : BaseTenantEntity<T>
        {
            DeleteInternal<TEntity, T>(entity);

            return _repository.SaveAsync();
        }

        private void DeleteInternal<TEntity, T>(TEntity entity) where TEntity : BaseTenantEntity<T>
        {
            foreach (var collection in GetCollectionProperties<TEntity, T>(entity))
            {
                var method = typeof(EntityServices).GetMethod("DeleteInternal", BindingFlags.NonPublic | BindingFlags.Instance);
                var generic = method.MakeGenericMethod(new Type[] { collection[0].GetType(), typeof(T) });

                foreach (BaseTenantEntity<T> childValue in collection)
                {
                    generic.Invoke(this, new[] { childValue });
                }
            }

            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;
        }

        private void SaveInternal<TEntity, T>(TEntity entity, bool modifyRepo) where TEntity : BaseTenantEntity<T>
        {
            foreach (var collection in GetCollectionProperties<TEntity, T>(entity))
            {
                var method = typeof(EntityServices).GetMethod("SaveInternal", BindingFlags.NonPublic | BindingFlags.Instance);
                var generic = method.MakeGenericMethod(new Type[] { collection[0].GetType(), typeof(T) });

                foreach (BaseTenantEntity<T> childValue in collection)
                {
                    generic.Invoke(this, new[] { childValue, (object)false });
                }
            }

            entity.Version++;
            entity.TenantId = _tenantId;

            if (entity.IsSaved)
            {
                entity.ModifiedOn = DateTime.Now;
                if (modifyRepo)
                {
                    _repository.Update<TEntity, T>(entity);
                }
            }
            else
            {
                entity.CreatedOn = DateTime.Now;
                if (modifyRepo)
                {
                    _repository.Add<TEntity, T>(entity);
                }
            }
        }

        private IEnumerable<System.Collections.IList> GetCollectionProperties<TEntity, T>(TEntity entity) where TEntity : BaseTenantEntity<T>
        {
            foreach (var propInfo in entity.GetType().GetProperties())
            {
                if (propInfo.PropertyType.IsGenericType
                    && typeof(ICollection<>).IsAssignableFrom(propInfo.PropertyType.GetGenericTypeDefinition()))
                {
                    var firstChildType = propInfo.PropertyType.GenericTypeArguments.FirstOrDefault();
                    if (typeof(BaseTenantEntity<T>).IsAssignableFrom(firstChildType))
                    {
                        var childValues = propInfo.GetValue(entity) as System.Collections.IList;
                        if (childValues.Count == 0)
                        {
                            continue;
                        }

                        yield return childValues;
                    }
                }
            }
        }
    }
}
