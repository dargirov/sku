﻿using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using Microsoft.EntityFrameworkCore;
using Product.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Bll
{
    public class ProductCategoryEntityServicePlugin : IEntityServicePlugin<ProductCategory>
    {
        private readonly IRepository _repository;

        public ProductCategoryEntityServicePlugin(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> OnDelete(ProductCategory productCategory, Messages messages)
        {
            var hasProducts = await _repository.GetQueryable<Entities.Product>()
                .Where(x => x.Category == productCategory)
                .AnyAsync();

            if (hasProducts)
            {
                messages.AddError("Cant delete category. It has products");
            }

            return !hasProducts;
        }

        public async Task<bool> OnSave(ProductCategory productCategory, Messages messages)
        {
            return true;
        }
    }
}
