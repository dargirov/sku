﻿using Infrastructure.Data.Common;
using Product.Bll.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Bll
{
    public interface IProductServices
    {
        Task<Entities.Product> GetByIdAsync(int id);
        Task<IEnumerable<Dtos.Api.ProductDto>> GetByOrganizationAndVariantCodeAsync(string organizationHash, string code);
        Task<(IEnumerable<Entities.Product> products, PageData pageSortData)> GetListAsync(int page, int pageSize, SortDirectionEnum dir);
        Task<(IEnumerable<Entities.Product> products, PageData pageSortData)> GetListAsync(int page, int pageSize, int column, SortDirectionEnum dir, string name, int? storeId, int? categoryId, int? manufacturerId, int? supplierId, string description);
        Task<List<Entities.ProductCategory>> GetCategoryListAsync();
        Task<int> EditAsync(Entities.Product product, IEnumerable<VariantDto> variants, Messages messages);
        Task<int> DeleteAsync(Entities.Product product);
        Task<Entities.ProductCategory> GetCategoryByIdAsync(int id);
        Task<int> EditCategoryAsync(Entities.ProductCategory category);
        Task<int> DeletePictureAsync(Entities.ProductPicture picture);
        Task<bool> DeleteCategoryAsync(Entities.ProductCategory productCategory, Messages messages);
        Task<bool> ImportAsync(IList<Dtos.Import.ProductDto> productDtos, Messages messages);
    }
}
