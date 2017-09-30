using Infrastructure.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Product.Entities
{
    public class ProductCategory : BaseTenantEntity<int>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
