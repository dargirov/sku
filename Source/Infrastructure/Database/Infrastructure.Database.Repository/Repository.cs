using Infrastructure.Data.Common;
using Infrastructure.Database.DbConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repository
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetQueryable<TEntity>(bool ignoreQueryFilters = false) where TEntity : BaseEntity
        {
            if (ignoreQueryFilters)
            {
                return _dbContext.Set<TEntity>().IgnoreQueryFilters();
            }

            return _dbContext.Set<TEntity>();
        }

        public Task<TEntity> GetByIdAsync<TEntity>(int id) where TEntity : BaseEntity
        {
            return _dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public Task<List<TEntity>> GetListAsync<TEntity>() where TEntity : BaseEntity
        {
            return GetListAsync<TEntity>(x => true);
        }

        public Task<List<TEntity>> GetListAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            return _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            var entry = _dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _dbContext.Set<TEntity>().Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public Task<int> SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public bool HasEntityChanges<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            return _dbContext.Entry(entity).State != EntityState.Unchanged;
        }

        public IEnumerable<(string name, string original, string current)> GetEntityChanges<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            var entry = _dbContext.Entry<TEntity>(entity);

            foreach (var property in entry.CurrentValues.Properties)
            {
                var currentValue = entry.CurrentValues[property];
                var originalValue = entry.OriginalValues[property];

                if (currentValue == null && originalValue == null)
                {
                    continue;
                }

                if ((currentValue == null && originalValue != null)
                    || (currentValue != null && originalValue == null))
                {
                    yield return GetResult(property.Name, originalValue, currentValue);
                    continue;
                }

                if (!currentValue.Equals(originalValue))
                {
                    yield return GetResult(property.Name, originalValue, currentValue);
                }

                //if (entry.State == EntityState.Added)
                //{
                //    yield return GetResult(property.Name, null, currentValue);
                //}
            }

            (string name, string original, string current) GetResult(string name, object originalObject, object currentObject)
            {
                return (name: name, original: originalObject?.ToString(), current: currentObject?.ToString());
            }
        }

        // TODO: expose commint transaction in method?
        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return _dbContext.Database.BeginTransactionAsync();
        }
    }
}
