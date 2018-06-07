using Infrastructure.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Product.Entities
{
    public class ProductCategory : BaseTenantEntity
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
