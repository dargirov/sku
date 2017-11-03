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

        public IQueryable<TEntity> GetQueryable<TEntity, T>(bool ignoreQueryFilters = false) where TEntity : BaseEntity<T>
        {
            if (ignoreQueryFilters)
            {
                return _dbContext.Set<TEntity>().IgnoreQueryFilters();
            }

            return _dbContext.Set<TEntity>();
        }

        public Task<TEntity> GetByIdAsync<TEntity, T>(T id) where TEntity : BaseEntity<T>
        {
            return _dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public Task<List<TEntity>> GetListAsync<TEntity, T>() where TEntity : BaseEntity<T>
        {
            return GetListAsync<TEntity, T>(x => true);
        }

        public Task<List<TEntity>> GetListAsync<TEntity, T>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity<T>
        {
            return _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public void Add<TEntity, T>(TEntity entity) where TEntity : BaseEntity<T>
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Update<TEntity, T>(TEntity entity) where TEntity : BaseEntity<T>
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

        public bool HasEntityChanges<TEntity, T>(TEntity entity) where TEntity : BaseEntity<T>
        {
            return _dbContext.Entry(entity).State != EntityState.Unchanged;
        }

        // TODO: expose commint transaction in method?
        public Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return _dbContext.Database.BeginTransactionAsync();
        }
    }
}
