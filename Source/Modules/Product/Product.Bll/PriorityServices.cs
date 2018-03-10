using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using Microsoft.EntityFrameworkCore;
using Product.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Product.Bll
{
    public class PriorityServices : IPriorityServices
    {
        private readonly IRepository _repository;
        private readonly IEntityServices _entityServices;

        public PriorityServices(IRepository repository, IEntityServices entityServices)
        {
            _repository = repository;
            _entityServices = entityServices;
        }

        public Task<List<ProductPriority>> GetListAsync(int productId)
        {
            return GetListAsync(productId, x => true);
        }

        public Task<List<ProductPriority>> GetListAsync(int productId, Expression<Func<ProductPriority, bool>> predicate)
        {
            return _repository
                .GetQueryable<ProductPriority, int>()
                .Include(x => x.User)
                .Include(x => x.Product)
                .Where(x => x.ProductId == productId)
                .Where(predicate)
                .ToListAsync();
        }

        public Task<int> EditAsync(ProductPriority priority)
        {
            return _entityServices.SaveAsync<ProductPriority, int>(priority);
        }
    }
}
