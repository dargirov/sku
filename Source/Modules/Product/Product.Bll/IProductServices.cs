using Infrastructure.Data.Common;
using Product.Bll.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.Bll
{
    public interface IProductServices
    {
        Task<Entities.Product> GetByIdAsync(int id);
        Task<IEnumerable<Dtos.Api.ProductDto>> GetByOrganizationAndVariantCodeAsync(string organizationHash, string code);
        Task<IEnumerable<Dtos.Api.ProductDto>> GetByOrganization(string organizationHash);
        Task<(IEnumerable<Entities.Product> products, PageData pageSortData)> GetListAsync(int page, int pageSize, SortDirectionEnum dir);
        Task<(IEnumerable<Entities.Product> products, PageData pageSortData)> GetListAsync(int page, int pageSize, int column, SortDirectionEnum dir, string name, int? storeId, int? categoryId, int? manufacturerId, int? supplierId, string description);
        Task<List<Entities.ProductCategory>> GetCategoryListAsync();
        Task<bool> EditAsync(Entities.Product product, IEnumerable<VariantDto> variants, Messages messages);
        Task<bool> DeleteAsync(Entities.Product product, Messages messages);
        Task<Entities.ProductCategory> GetCategoryByIdAsync(int id);
        Task<bool> EditCategoryAsync(Entities.ProductCategory category, Messages messages);
        Task<bool> DeletePictureAsync(Entities.ProductPicture picture, Messages messages);
        Task<bool> DeleteCategoryAsync(Entities.ProductCategory productCategory, Messages messages);
        Task<bool> ImportAsync(IList<Dtos.Import.ProductDto> productDtos, Messages messages);
        Task<Entities.ProductSettings> GetSettingsAsync();
        Task<bool> EditSettingsAsync(Entities.ProductSettings settings, Messages messages);
    }
}
