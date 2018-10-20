﻿using Administration.Entities;
using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Multitenancy;
using StructureMap;
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
        private readonly ITenantProvider _tenantProvider;
        private readonly ICacheServices _cacheServices;
        private readonly IContainer _container;

        public EntityServices(IRepository repository, ITenantProvider tenantProvider, ICacheServices cacheServices, IContainer container)
        {
            _repository = repository;
            _tenantProvider = tenantProvider;
            _cacheServices = cacheServices;
            _container = container;
        }

        // TODO: use messages?
        public async Task<bool> SaveAsync<TEntity>(TEntity entity, Messages messages) where TEntity : BaseTenantEntity
        {
            var visited = new EntityVisitTree();
            visited.Add<TEntity>(entity);

            using (var transaction = await _repository.BeginTransactionAsync())
            {
                SaveMemoInternal<TEntity>(entity);

                var saveError = new OperationResult();
                await SaveInternal<TEntity>(entity, true, visited, messages, saveError);
                if (saveError.HasError)
                {
                    transaction.Rollback();
                    return false;
                }

                await _repository.SaveAsync();
                transaction.Commit();
            }

            return true;
        }

        public async Task<bool> DeleteAsync<TEntity>(TEntity entity, Messages messages) where TEntity : BaseTenantEntity
        {
            var visited = new EntityVisitTree();
            visited.Add<TEntity>(entity);

            using (var transaction = await _repository.BeginTransactionAsync())
            {
                var deleteError = new OperationResult();
                await DeleteInternal<TEntity>(entity, true, visited, messages, deleteError);
                if (deleteError.HasError)
                {
                    transaction.Rollback();
                    return false;
                }

                await _repository.SaveAsync();
                transaction.Commit();
            }

            return true;
        }

        public EntityChangeTree CreateEntityChangeTree<TEntity>(TEntity entity) where TEntity : BaseTenantEntity
        {
            var changes = _repository.GetEntityChanges<TEntity>(entity);
            var tree = new EntityChangeTree();

            var visited = new EntityVisitTree();
            visited.Add<TEntity>(entity);

            CreateEntityChangeTreeInternal<TEntity>(entity, tree, visited, true);

            return tree;
        }

        private void SaveMemoInternal<TEntity>(TEntity entity) where TEntity : BaseTenantEntity
        {
            var tree = CreateEntityChangeTree<TEntity>(entity);
            var now = Time.Now;
            var baseEntityId = entity.Id;
            var baseEntityName = entity.GetType().Name;
            var changedById = _cacheServices.Get<int>("user_id");

            WalkTree(tree);

            void WalkTree(EntityChangeTree t)
            {
                foreach (var entityChange in t.Entity)
                {
                    var memo = new Memo()
                    {
                        BaseEntityName = baseEntityName,
                        BaseEntityId = baseEntityId,
                        ChangedOn = now,
                        EntityId = entityChange.Entity.Id,
                        EntityName = entityChange.Entity.GetType().Name,
                        OldValue = entityChange.Original,
                        NewValue = entityChange.Current,
                        Property = entityChange.Property,
                        CreatedOn = now,
                        Version = 1,
                        TenantId = _tenantProvider.GetTenantId(),
                        ChangedById = changedById
                    };

                    _repository.Add(memo);
                }

                foreach (var child in t.Children)
                {
                    WalkTree(child);
                }
            }
        }

        // TODO: does not work when adding (or removing?) new entity to collection
        private void CreateEntityChangeTreeInternal<TEntity>(TEntity entity, EntityChangeTree tree, EntityVisitTree visited, bool isBaseEntity) where TEntity : BaseTenantEntity
        {
            var method = typeof(EntityServices).GetMethod(nameof(CreateEntityChangeTreeInternal), BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var property in GetProperties<TEntity>(entity))
            {
                var generic = method.MakeGenericMethod(new Type[] { property.GetType() });
                if (!visited.IsVisited<BaseTenantEntity>(property))
                {
                    visited.Add<BaseTenantEntity>(property);
                    generic.Invoke(this, new object[] { property, tree, visited, false });
                }
            }

            // TODO: does nothing when the whole item in the collection is removed
            foreach (var collection in GetCollectionProperties<TEntity>(entity))
            {
                var generic = method.MakeGenericMethod(new Type[] { collection[0].GetType() });
                foreach (BaseTenantEntity childValue in collection)
                {
                    if (!visited.IsVisited<BaseTenantEntity>(childValue))
                    {
                        visited.Add<BaseTenantEntity>(childValue);
                        generic.Invoke(this, new object[] { childValue, tree, visited, false });
                    }
                }
            }

            var changes = _repository.GetEntityChanges<TEntity>(entity);
            if (changes.Count() > 0)
            {
                tree.Add(changes.Select(x => new EntityChange(entity, x.name, x.original, x.current)).ToList(), isBaseEntity);
            }
        }

        private async Task SaveInternal<TEntity>(TEntity entity, bool modifyRepo, EntityVisitTree visited, Messages messages, OperationResult operationResult) where TEntity : BaseTenantEntity
        {
            var method = typeof(EntityServices).GetMethod(nameof(SaveInternal), BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var property in GetProperties<TEntity>(entity))
            {
                var generic = method.MakeGenericMethod(new Type[] { property.GetType() });
                if (!visited.IsVisited<BaseTenantEntity>(property))
                {
                    visited.Add<BaseTenantEntity>(property);
                    generic.Invoke(this, new[] { property, (object)false, visited, messages, operationResult });
                }
            }

            foreach (var collection in GetCollectionProperties<TEntity>(entity))
            {
                var generic = method.MakeGenericMethod(new Type[] { collection[0].GetType() });
                foreach (BaseTenantEntity childValue in collection)
                {
                    // TODO: check why using !childValue.IsSaved
                    if (!childValue.IsSaved || (!visited.IsVisited<BaseTenantEntity>(childValue)))
                    {
                        visited.Add<BaseTenantEntity>(childValue);
                        generic.Invoke(this, new[] { childValue, (object)false, visited, messages, operationResult });
                    }
                }
            }

            if (entity.IsSaved && !_repository.HasEntityChanges<BaseTenantEntity>(entity))
            {
                return;
            }

            foreach (var plugin in _container.GetAllInstances<IEntityServicePlugin<TEntity>>())
            {
                if (!await plugin.OnSave(entity, messages))
                {
                    operationResult.AddError();
                }
            }

            entity.Version++;
            entity.TenantId = _tenantProvider.GetTenantId();

            if (entity.IsSaved)
            {
                entity.ModifiedOn = Time.Now;
                if (modifyRepo)
                {
                    _repository.Update<TEntity>(entity);
                }
            }
            else
            {
                entity.CreatedOn = Time.Now;
                if (modifyRepo)
                {
                    _repository.Add<TEntity>(entity);
                }
            }
        }

        private async Task DeleteInternal<TEntity>(TEntity entity, bool modifyRepo, EntityVisitTree visited, Messages messages, OperationResult operationResult) where TEntity : BaseTenantEntity
        {
            var method = typeof(EntityServices).GetMethod(nameof(DeleteInternal), BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var collection in GetCollectionProperties<TEntity>(entity))
            {
                var generic = method.MakeGenericMethod(new Type[] { collection[0].GetType() });
                foreach (BaseTenantEntity childValue in collection)
                {
                    if (!visited.IsVisited<BaseTenantEntity>(childValue))
                    {
                        visited.Add<BaseTenantEntity>(childValue);
                        generic.Invoke(this, new[] { childValue, (object)false, visited, messages, operationResult });
                    }
                }
            }

            foreach (var plugin in _container.GetAllInstances<IEntityServicePlugin<TEntity>>())
            {
                if (!await plugin.OnDelete(entity, messages))
                {
                    operationResult.AddError();
                }
            }

            entity.IsDeleted = true;
            entity.DeletedOn = Time.Now;

            if (modifyRepo)
            {
                _repository.Update<TEntity>(entity);
            }
        }

        private IEnumerable<BaseTenantEntity> GetProperties<TEntity>(TEntity entity) where TEntity : BaseTenantEntity
        {
            foreach (var propInfo in entity.GetType().GetProperties())
            {
                if (propInfo.PropertyType.IsClass
                    && typeof(BaseTenantEntity).IsAssignableFrom(propInfo.PropertyType))
                {
                    if (propInfo.GetValue(entity) is BaseTenantEntity value)
                    {
                        yield return value;
                    }
                }
            }
        }

        private IEnumerable<System.Collections.IList> GetCollectionProperties<TEntity>(TEntity entity) where TEntity : BaseTenantEntity
        {
            foreach (var propInfo in entity.GetType().GetProperties())
            {
                if (propInfo.PropertyType.IsGenericType
                    && typeof(ICollection<>).IsAssignableFrom(propInfo.PropertyType.GetGenericTypeDefinition()))
                {
                    var firstChildType = propInfo.PropertyType.GenericTypeArguments.FirstOrDefault();
                    if (typeof(BaseTenantEntity).IsAssignableFrom(firstChildType))
                    {
                        // TODO: check if this works
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

        private class EntityVisitTree
        {
            private readonly HashSet<string> _visited;

            public EntityVisitTree()
            {
                _visited = new HashSet<string>();
            }

            public void Add<TEntity>(TEntity entity) where TEntity : BaseTenantEntity
            {
                var identifier = GetIdentifier<TEntity>(entity);
                _visited.Add(identifier);
            }

            public bool IsVisited<TEntity>(TEntity entity) where TEntity : BaseTenantEntity
            {
                var identifier = GetIdentifier<TEntity>(entity);
                return _visited.Contains(identifier);
            }

            private string GetIdentifier<TEntity>(TEntity entity) where TEntity : BaseTenantEntity
            {
                var type = entity.GetType();
                return $"{type.Name} - {type.GUID} - {entity.Id}";
            }
        }

        private class OperationResult
        {
            public bool HasError { get; private set; }
            public void AddError() => HasError = true;
        }
    }
}
