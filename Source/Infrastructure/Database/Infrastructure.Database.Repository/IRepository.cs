using Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repository
{
    public interface IRepository
    {
        IQueryable<TEntity> GetQueryable<TEntity, T>(bool ignoreQueryFilters = false) where TEntity : BaseEntity<T>;

        Task<TEntity> GetByIdAsync<TEntity, T>(T id) where TEntity : BaseEntity<T>;

        Task<List<TEntity>> GetListAsync<TEntity, T>() where TEntity : BaseEntity<T>;
        Task<List<TEntity>> GetListAsync<TEntity, T>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity<T>;

        void Add<TEntity, T>(TEntity entity) where TEntity : BaseEntity<T>;

        void Update<TEntity, T>(TEntity entity) where TEntity : BaseEntity<T>;

        Task<int> SaveAsync();

        bool HasEntityChanges<TEntity, T>(TEntity entity) where TEntity : BaseEntity<T>;

        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
