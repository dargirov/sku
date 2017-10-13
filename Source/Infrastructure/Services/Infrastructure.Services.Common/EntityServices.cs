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
            var visited = new EntityVisitedTree();
            visited.Add<TEntity, T>(entity);

            SaveInternal<TEntity, T>(entity, true, visited);

            return _repository.SaveAsync();
        }

        public Task<int> DeleteAsync<TEntity, T>(TEntity entity) where TEntity : BaseTenantEntity<T>
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;

            return _repository.SaveAsync();
        }

        private void SaveInternal<TEntity, T>(TEntity entity, bool modifyRepo, EntityVisitedTree visited) where TEntity : BaseTenantEntity<T>
        {
            var method = typeof(EntityServices).GetMethod("SaveInternal", BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var property in GetProperties<TEntity, T>(entity))
            {
                var generic = method.MakeGenericMethod(new Type[] { property.GetType(), typeof(T) });
                if (!property.IsSaved || (!visited.IsVisited<BaseTenantEntity<T>, T>(property)/* && _repository.HasEntityChanges<BaseTenantEntity<T>, T>(property)*/))
                {
                    visited.Add<BaseTenantEntity<T>, T>(property);
                    generic.Invoke(this, new[] { property, (object)false, visited });
                }
            }

            foreach (var collection in GetCollectionProperties<TEntity, T>(entity))
            {
                var generic = method.MakeGenericMethod(new Type[] { collection[0].GetType(), typeof(T) });
                foreach (BaseTenantEntity<T> childValue in collection)
                {
                    if (!childValue.IsSaved || (!visited.IsVisited<BaseTenantEntity<T>, T>(childValue) /*&& _repository.HasEntityChanges<BaseTenantEntity<T>, T>(childValue)*/))
                    {
                        visited.Add<BaseTenantEntity<T>, T>(childValue);
                        generic.Invoke(this, new[] { childValue, (object)false, visited });
                    }
                }
            }

            if (entity.IsSaved && !_repository.HasEntityChanges<BaseTenantEntity<T>, T>(entity))
            {
                return;
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

        private IEnumerable<BaseTenantEntity<T>> GetProperties<TEntity, T>(TEntity entity) where TEntity : BaseTenantEntity<T>
        {
            foreach (var propInfo in entity.GetType().GetProperties())
            {
                if (propInfo.PropertyType.IsClass
                    && typeof(BaseTenantEntity<T>).IsAssignableFrom(propInfo.PropertyType))
                {
                    if (propInfo.GetValue(entity) is BaseTenantEntity<T> value)
                    {
                        yield return value;
                    }
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
                        if (childValues == null || childValues.Count == 0)
                        {
                            continue;
                        }

                        yield return childValues;
                    }
                }
            }
        }

        private class EntityVisitedTree
        {
            private readonly HashSet<string> _visited;

            public EntityVisitedTree() 
            {
                _visited = new HashSet<string>();
            }

            public void Add<TEntity, T>(TEntity entity) where TEntity : BaseTenantEntity<T>
            {
                var identifier = GetIdentifier<TEntity, T>(entity);
                _visited.Add(identifier);
            }

            public bool IsVisited<TEntity, T>(TEntity entity) where TEntity : BaseTenantEntity<T>
            {
                var identifier = GetIdentifier<TEntity, T>(entity);
                return _visited.Contains(identifier);
            }

            private string GetIdentifier<TEntity, T>(TEntity entity) where TEntity : BaseTenantEntity<T>
            {
                var type = entity.GetType();
                return $"{type.Name} - {type.GUID} - {entity.Id}";
            }
        }
    }
}
