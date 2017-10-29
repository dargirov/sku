using Infrastructure.Data.Common;
using System.Threading.Tasks;

namespace Product.Bll
{
    public interface IProductCategoryEntityPlugin
    {
        Task<bool> OnSave(Entities.ProductCategory productCategory, Messages messages);
        Task<bool> OnDelete(Entities.ProductCategory productCategory, Messages messages);
    }
}
