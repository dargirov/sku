﻿using Infrastructure.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Product.Bll
{
    public interface IPriorityServices
    {
        Task<List<Entities.ProductPriority>> GetListAsync(int productId);
        Task<List<Entities.ProductPriority>> GetListAsync(int productId, Expression<Func<Entities.ProductPriority, bool>> predicate);
        Task<bool> EditAsync(Entities.ProductPriority priority, Messages messages);
    }
}
