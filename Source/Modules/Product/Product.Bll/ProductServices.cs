using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Bll
{
    public class ProductServices : IProductServices
    {
        private readonly IRepository _repository;
        private readonly IEntityServices _entityServices;

        public ProductServices(IRepository repository, IEntityServices entityServices)
        {
            _repository = repository;
            _entityServices = entityServices;
        }

        public async Task<Entities.Product> GetByIdAsync(int id)
        {
            var product = await _repository.GetQueryable<Entities.Product, int>()
                .Include(x => x.Supplier)
                .Include(x => x.Category)
                .Include(x => x.Manufacturer)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return null;
            }

            product.Variants = await _repository.GetListAsync<Entities.ProductVariant, int>(x => x.ProductId == product.Id);
            foreach (var variant in product.Variants)
            {
                variant.Stocks = await _repository.GetListAsync<Entities.ProductStock, int>(x => x.ProductVariantId == variant.Id);
            }

            product.Pictures = await _repository.GetQueryable<Entities.ProductPicture, int>()
                //.Include(p => p.FullSize)
                //.Include(p => p.Thumb)
                .Where(x => x.ProductId == product.Id)
                .ToListAsync();

            return product;
        }

        public async Task<(IEnumerable<Entities.Product> products, int count)> GetListAsync(int start, int limit, string column, string dir, string name, int? categoryId, int? supplierId, string warranty, string description)
        {
            var query = _repository.GetQueryable<Entities.Product, int>()
                .Include(x => x.Supplier)
                .Include(x => x.Category)
                .Include(x => x.Manufacturer)
                .Where(x => true);

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            if (supplierId.HasValue && supplierId.Value > 0)
            {
                query = query.Where(p => p.SupplierId == supplierId.Value);
            }

            if (!string.IsNullOrWhiteSpace(warranty))
            {
                query = query.Where(p => p.Warranty.Contains(warranty));
            }

            if (!string.IsNullOrWhiteSpace(description))
            {
                query = query.Where(p => p.Description.Contains(description));
            }

            switch (column.ToString())
            {
                case "2":
                    query = dir == "desc"
                        ? query.OrderByDescending(x => x.Name)
                        : query.OrderBy(x => x.Name);
                    break;
                case "3":
                    query = dir == "desc"
                        ? query.OrderByDescending(x => x.Category.Name)
                        : query.OrderBy(x => x.Category.Name);
                    break;
                case "4":
                    query = dir == "desc"
                        ? query.OrderByDescending(x => x.Supplier.Name)
                        : query.OrderBy(x => x.Supplier.Name);
                    break;
                case "5":
                    query = dir == "desc"
                        ? query.OrderByDescending(x => x.Id)
                        : query.OrderBy(x => x.Id);
                    break;
            }

            var products = await query
                .Skip(start)
                .Take(limit)
                .ToListAsync();

            foreach (var product in products)
            {
                //product.Supplier = await _repository.GetQueryable<Supplier.Entities.Supplier, int>().Where(x => x.Id == product.SupplierId).FirstOrDefaultAsync();
                product.Variants = await _repository.GetListAsync<Entities.ProductVariant, int>(x => x.ProductId == product.Id);
                product.Pictures = await _repository.GetListAsync<Entities.ProductPicture, int>(x => x.ProductId == product.Id);
                foreach (var variant in product.Variants)
                {
                    variant.Stocks = await _repository.GetListAsync<Entities.ProductStock, int>(x => x.ProductVariantId == variant.Id);
                }

                foreach (var picture in product.Pictures)
                {
                    //picture.FullSize = await filesService.GetByIdAsync(picture.FullSizeId);
                    //picture.Thumb = await filesService.GetByIdAsync(picture.ThumbId);
                }
            }

            var count = await query.CountAsync();
            return (products: products, count: count);
        }

        public async Task<int> EditAsync(Entities.Product product)
        {
            //if (product.IsSaved)
            //{
            //    var currentProduct = await this.products.GetByIdAsync(product.Id);
            //    currentProduct.Name = product.Name;
            //    currentProduct.Warranty = product.Warranty;
            //    currentProduct.ProductCategoryId = product.ProductCategoryId;
            //    currentProduct.Description = product.Description;
            //    currentProduct.Pictures = product.Pictures;
            //    currentProduct.ManufacturerId = product.ManufacturerId;
            //    currentProduct.SupplierId = product.SupplierId;

            //    // delete all variants and stocks
            //    foreach (var variant in currentProduct.Variants)
            //    {
            //        variant.IsDeleted = true;
            //        variant.DeletedOn = DateTime.Now;
            //        foreach (var stock in variant.Stocks)
            //        {
            //            stock.IsDeleted = true;
            //            stock.DeletedOn = DateTime.Now;
            //        }
            //    }

            //    var newVariants = new HashSet<ProductVariant>();

            //    foreach (var variant in product.Variants)
            //    {
            //        var currentVariant = currentProduct.Variants.FirstOrDefault(x => x.Id == variant.Id);
            //        if (currentVariant != null)
            //        {
            //            currentVariant.Code = variant.Code;
            //            currentVariant.ModifiedOn = DateTime.Now;
            //            currentVariant.PriceNet = variant.PriceNet;
            //            currentVariant.PriceNetType = variant.PriceNetType;
            //            currentVariant.PriceCustomer = variant.PriceCustomer;
            //            currentVariant.PriceCustomerType = variant.PriceCustomerType;
            //            currentVariant.IsDeleted = false;
            //            currentVariant.DeletedOn = null;
            //        }
            //        else
            //        {
            //            newVariants.Add(variant);
            //            continue;
            //        }

            //        if (variant.Stocks != null)
            //        {
            //            foreach (var stock in variant.Stocks)
            //            {
            //                var currentStock = currentVariant.Stocks.FirstOrDefault(x => x.Id == stock.Id);
            //                if (currentStock != null)
            //                {
            //                    currentStock.StoreId = stock.StoreId;
            //                    currentStock.Quantity = stock.Quantity;
            //                    currentStock.QuantityMeasureType = stock.QuantityMeasureType;
            //                    currentStock.LowQuantity = stock.LowQuantity;
            //                    currentStock.LowQuantityMeasureType = stock.LowQuantityMeasureType;
            //                    currentStock.IsDeleted = false;
            //                    currentStock.DeletedOn = null;
            //                }
            //                else
            //                {
            //                    currentStock = stock;
            //                    currentVariant.Stocks.Add(currentStock);
            //                }
            //            }
            //        }
            //    }

            //    foreach (var variant in newVariants)
            //    {
            //        currentProduct.Variants.Add(variant);
            //    }

            //    this.products.Update(currentProduct);
            //}
            //else
            //{
            //    _repository.SaveAsync(product);
            //}

            return await _entityServices.SaveAsync<Entities.Product, int>(product);
        }

        public Task<List<Entities.ProductCategory>> GetCategoryListAsync()
        {
            return _repository.GetQueryable<Entities.ProductCategory, int>()
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public Task<Entities.ProductCategory> GetCategoryByIdAsync(int id)
        {
            return _repository.GetByIdAsync<Entities.ProductCategory, int>(id);
        }

        public Task<int> EditCategoryAsync(Entities.ProductCategory category)
        {
            return _entityServices.SaveAsync<Entities.ProductCategory, int>(category);
        }

        public Task<int> DeleteAsync(Entities.Product product)
        {
            return _entityServices.DeleteAsync<Entities.Product, int>(product);
        }
    }
}
