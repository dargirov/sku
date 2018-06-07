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
        IQueryable<TEntity> GetQueryable<TEntity>(bool ignoreQueryFilters = false) where TEntity : BaseEntity;

        Task<TEntity> GetByIdAsync<TEntity>(int id) where TEntity : BaseEntity;

        Task<List<TEntity>> GetListAsync<TEntity>() where TEntity : BaseEntity;
        Task<List<TEntity>> GetListAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity;

        void Add<TEntity>(TEntity entity) where TEntity : BaseEntity;

        void Update<TEntity>(TEntity entity) where TEntity : BaseEntity;

        Task<int> SaveAsync();

        bool HasEntityChanges<TEntity>(TEntity entity) where TEntity : BaseEntity;
        IEnumerable<(string name, string original, string current)> GetEntityChanges<TEntity>(TEntity entity) where TEntity : BaseEntity;

        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
