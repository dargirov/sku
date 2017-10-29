using Administration.Bll;
using AutoMapper;
using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using Infrastructure.Services.Common.Mappings;
using Manufacturer.Bll;
using Microsoft.EntityFrameworkCore;
using Product.Bll.Dtos;
using Store.Bll;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Bll
{
    public class ProductServices : IProductServices
    {
        private readonly IContainer _container;
        private readonly IRepository _repository;
        private readonly IEntityServices _entityServices;
        private readonly IStoreServices _storeServices;
        private readonly IManufacturerServices _manufacturerServices;
        private readonly ICountryServices _countryServices;
        private IMapper Mapper => AutoMapperConfig.Mapper;

        public ProductServices(IContainer container, IRepository repository, IEntityServices entityServices, IStoreServices storeServices, IManufacturerServices manufacturerServices, ICountryServices countryServices)
        {
            _container = container;
            _repository = repository;
            _entityServices = entityServices;
            _storeServices = storeServices;
            _manufacturerServices = manufacturerServices;
            _countryServices = countryServices;
        }

        public async Task<Entities.Product> GetByIdAsync(int id)
        {
            var storeReadIds = (await _storeServices.GetStoreListWithReadPrivilegeAsync()).Select(x => x.Id);

            var product = await _repository.GetQueryable<Entities.Product, int>()
                .Include(x => x.Supplier)
                .Include(x => x.Category)
                .Include(x => x.Manufacturer)
                .Include(x => x.Pictures).ThenInclude(x => x.FullSize)
                .Include(x => x.Pictures).ThenInclude(x => x.Thumb)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return null;
            }

            product.Variants = await _repository.GetListAsync<Entities.ProductVariant, int>(x => x.ProductId == product.Id);
            foreach (var variant in product.Variants)
            {
                variant.Stocks = await _repository.GetQueryable<Entities.ProductStock, int>()
                    .Include(x => x.Store)
                    .Where(x => x.VariantId == variant.Id && storeReadIds.Contains(x.StoreId))
                    .ToListAsync();
            }

            product.Pictures = await _repository.GetQueryable<Entities.ProductPicture, int>()
                .Include(p => p.FullSize)
                .Include(p => p.Thumb)
                .Where(x => x.ProductId == product.Id)
                .ToListAsync();

            return product;
        }

        public async Task<(IEnumerable<Entities.Product> products, int count)> GetListAsync(int start, int limit, string column, string dir, string name, int? categoryId, int? manufacturerId, int? supplierId, string warranty, string description)
        {
            var storeIds = (await _storeServices.GetListAsync()).Select(x => x.Id);

            var query = _repository.GetQueryable<Entities.Product, int>()
                .Include(x => x.Supplier)
                .Include(x => x.Category)
                .Include(x => x.Manufacturer)
                .Include(x => x.Pictures).ThenInclude(x => x.FullSize)
                .Include(x => x.Pictures).ThenInclude(x => x.Thumb)
                .Include(x => x.Variants).ThenInclude(x => x.Stocks)
                .Where(x => x.Variants.Any(v => v.Stocks.Any(s => storeIds.Contains(s.StoreId))));

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(p => p.Name.Contains(name));
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            if (manufacturerId.HasValue && manufacturerId.Value > 0)
            {
                query = query.Where(p => p.ManufacturerId == manufacturerId.Value);
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

            //foreach (var product in products)
            //{
            //    //product.Supplier = await _repository.GetQueryable<Supplier.Entities.Supplier, int>().Where(x => x.Id == product.SupplierId).FirstOrDefaultAsync();
            //    product.Variants = await _repository.GetListAsync<Entities.ProductVariant, int>(x => x.ProductId == product.Id);
            //    product.Pictures = await _repository.GetListAsync<Entities.ProductPicture, int>(x => x.ProductId == product.Id);
            //    foreach (var variant in product.Variants)
            //    {
            //        variant.Stocks = await _repository.GetListAsync<Entities.ProductStock, int>(x => x.VariantId == variant.Id);
            //    }

            //    foreach (var picture in product.Pictures)
            //    {
            //        //picture.FullSize = await filesService.GetByIdAsync(picture.FullSizeId);
            //        //picture.Thumb = await filesService.GetByIdAsync(picture.ThumbId);
            //    }
            //}

            var count = await query.CountAsync();
            return (products: products, count: count);
        }

        public async Task<int> EditAsync(Entities.Product product, IEnumerable<VariantDto> variantDtos, Messages messages)
        {
            var storeWriteIds = (await _storeServices.GetStoreListWithWritePrivilegeAsync()).Select(x => x.Id);
            var storeDeleteIds = (await _storeServices.GetStoreListWithDeletePrivilegeAsync()).Select(x => x.Id);

            if (!product.IsSaved)
            {
                product.Variants = new List<Entities.ProductVariant>();
                foreach (var variantDto in variantDtos)
                {
                    // TODO: add check if the user has priv to insert into the store
                    var variant = Mapper.Map<Entities.ProductVariant>(variantDto);
                    variant.Stocks = new List<Entities.ProductStock>();
                    foreach (var stockDto in variantDto.Stocks)
                    {
                        variant.Stocks.Add(Mapper.Map<Entities.ProductStock>(stockDto));
                    }

                    product.Variants.Add(variant);
                }

                return await _entityServices.SaveAsync<Entities.Product, int>(product);
            }

            for (var i = 0; i < product.Variants.Count; i++)
            {
                var variant = product.Variants.Skip(i).Take(1).First();
                var variantDto = variantDtos.FirstOrDefault(x => x.Id == variant.Id);
                if (variantDto != null)
                {
                    // TODO: what priv to check here?
                    variant.Code = variantDto.Code;
                    variant.PriceNet = variantDto.PriceNet;
                    variant.PriceNetType = variantDto.PriceNetType;
                    variant.PriceCustomer = variantDto.PriceCustomer;
                    variant.PriceCustomerType = variantDto.PriceCustomerType;

                    for (var j = 0; j < variant.Stocks.Count; j++)
                    {
                        var stock = variant.Stocks.Skip(j).Take(1).First();
                        var stockDto = variantDto.Stocks?.FirstOrDefault(x => x.Id == stock.Id);
                        if (stockDto != null)
                        {
                            if (storeWriteIds.Contains(stock.StoreId))
                            {
                                stock.Quantity = stockDto.Quantity;
                                stock.QuantityMeasureType = stockDto.QuantityMeasureType;
                                stock.LowQuantity = stockDto.LowQuantity;
                                stock.LowQuantityMeasureType = stockDto.LowQuantityMeasureType;
                            }
                        }
                        else
                        {
                            if (storeDeleteIds.Contains(stock.StoreId))
                            {
                                await _entityServices.DeleteAsync<Entities.ProductStock, int>(stock);
                            }
                            else
                            {
                                messages.AddWarning($"Cant delete stock from store {stock.Store.Name}. You dont have delete permissions.");
                            }
                        }
                    }
                }
                else
                {
                    // TODO: I should probably check product write priv?
                    if (variant.Stocks.All(x => storeDeleteIds.Contains(x.StoreId)))
                    {
                        await _entityServices.DeleteAsync<Entities.ProductVariant, int>(variant);
                    }
                    else
                    {
                        messages.AddWarning($"Cant delete variant because it will delete stock in stores for which you dont have delete permission.");
                    }
                }
            }

            for (int i = 0; i < variantDtos.Count(); i++)
            {
                var variantDto = variantDtos.Skip(i).Take(1).First();
                var variant = product.Variants.FirstOrDefault(x => x.Id == variantDto.Id && x.IsSaved);
                for (int j = 0; j < (variantDto.Stocks?.Count() ?? 0); j++)
                {
                    var stockDto = variantDto.Stocks.Skip(j).Take(1).First();
                    var stock = variant?.Stocks.FirstOrDefault(x => x.Id == stockDto.Id && x.IsSaved);
                    if (storeWriteIds.Contains(stockDto.StoreId))
                    {
                        if (variant == null)
                        {
                            variant = Mapper.Map<Entities.ProductVariant>(variantDto);
                            product.Variants.Add(variant);
                            variant.Stocks = new List<Entities.ProductStock>();
                        }

                        if (stock == null)
                        {
                            variant.Stocks.Add(Mapper.Map<Entities.ProductStock>(stockDto));
                        }
                        else
                        {
                            stock.Quantity = stockDto.Quantity;
                            stock.QuantityMeasureType = stockDto.QuantityMeasureType;
                            stock.LowQuantity = stockDto.LowQuantity;
                            stock.LowQuantityMeasureType = stockDto.LowQuantityMeasureType;
                        }
                    }
                    else
                    {
                        if (stockDto.LowQuantity != stock.LowQuantity
                            || stockDto.Quantity != stock.Quantity
                            || stockDto.LowQuantityMeasureType != stock.LowQuantityMeasureType
                            || stockDto.QuantityMeasureType != stock.QuantityMeasureType)
                        {
                            var storeName = stock?.Store?.Name
                                ?? (await _repository.GetByIdAsync<Store.Entities.Store, int>(stockDto.StoreId)).Name;
                            messages.AddWarning($"Cant make changes to store {storeName}. You dont have write permissions.");
                        }
                    }
                }
            }

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

        public Task<int> DeletePictureAsync(Entities.ProductPicture picture)
        {
            return _entityServices.DeleteAsync<Entities.ProductPicture, int>(picture);
        }

        public async Task<bool> DeleteCategoryAsync(Entities.ProductCategory productCategory, Messages messages)
        {
            foreach (var plugin in _container.GetAllInstances<IProductCategoryEntityPlugin>())
            {
                if (!await plugin.OnDelete(productCategory, messages))
                {
                    return false;
                }
            }

            var result = await _entityServices.DeleteAsync<Entities.ProductCategory, int>(productCategory);

            return result != 0;
        }

        public async Task<bool> ImportAsync(IList<Dtos.Import.ProductDto> productDtos, Messages messages)
        {
            var stores = await _storeServices.GetListAsync();
            var countries = await _countryServices.GetListAsync();

            using (var transaction = await _repository.BeginTransactionAsync())
            {
                if (!await SaveProductsAsync(productDtos, stores, countries, messages))
                {
                    return false;
                }

                transaction.Commit();
            }

            return true;
        }

        private async Task<bool> SaveProductsAsync(IList<Dtos.Import.ProductDto> productDtos, IList<Store.Entities.Store> stores, IList<Administration.Entities.Country> countries, Messages messages)
        {
            var categories = await GetCategoryListAsync();
            var manufacturers = await _manufacturerServices.GetListAsync();

            foreach (var productDto in productDtos)
            {
                if (string.IsNullOrEmpty(productDto.Name)
                    || string.IsNullOrEmpty(productDto.Category)
                    || string.IsNullOrEmpty(productDto.Description))
                {
                    messages.AddWarning("Invalid data for product");
                    continue;
                }

                if (productDto.Manufacturer == null
                    || string.IsNullOrEmpty(productDto.Manufacturer.Name)
                    || string.IsNullOrEmpty(productDto.Manufacturer.Country))
                {
                    messages.AddWarning($"Invalid manufacturer data for product {productDto.Name}");
                    continue;
                }

                if (!countries.Any(x => x.Name == productDto.Manufacturer.Country))
                {
                    messages.AddWarning($"Country with name {productDto.Manufacturer.Country} is not found");
                    continue;
                }

                if (productDto.Variants == null
                    || productDto.Variants.Count == 0)
                {
                    messages.AddWarning($"Product {productDto.Name} has no variants");
                    continue;
                }

                var product = new Entities.Product()
                {
                    Name = LimitString(productDto.Name, 300),
                    Description = LimitString(productDto.Description, 1000),
                    Category = categories.FirstOrDefault(x => x.Name == productDto.Category)
                        ?? new Entities.ProductCategory() { Name = LimitString(productDto.Category, 100) },
                    Manufacturer = manufacturers.FirstOrDefault(x => x.Name == productDto.Manufacturer.Name)
                        ?? new Manufacturer.Entities.Manufacturer() { Name = LimitString(productDto.Manufacturer.Name, 100), Country = countries.First(x => x.Name == productDto.Manufacturer.Country) },
                    Variants = productDto.Variants.Select(x => new Entities.ProductVariant()
                    {
                        Code = LimitString(x.SupplierStock, 50),
                        PriceCustomer = decimal.Parse(x.PriceCustomer),
                        PriceCustomerType = Entities.CurrencyTypeEnum.BGN,
                        PriceNet = decimal.Parse(x.PriceSupplierNet),
                        PriceNetType = Entities.CurrencyTypeEnum.BGN,
                        Stocks = new List<Entities.ProductStock>()
                            {
                                new Entities.ProductStock()
                                {
                                    Quantity = int.Parse(x.Quantity),
                                    QuantityMeasureType = Entities.MeasureTypeEnum.Quantity,
                                    LowQuantity = Math.Max(int.Parse(x.Quantity) / 2, 1),
                                    LowQuantityMeasureType = Entities.MeasureTypeEnum.Quantity,
                                    Store = stores.First()
                                }
                            }
                    }).ToList()
                };

                try
                {
                    await _entityServices.SaveAsync<Entities.Product, int>(product);
                }
                catch (Exception ex)
                {
                    Debug.Fail(ex.Message);
                    return false;
                }
            }

            string LimitString(string text, int length)
            {
                if (text.Length > length)
                {
                    return text.Substring(0, length);
                }

                return text;
            }

            return true;
        }
    }
}
