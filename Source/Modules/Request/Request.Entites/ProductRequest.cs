using Infrastructure.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Request.Entites
{
    public class ProductRequest : BaseTenantEntity<int>
    {
        [Required]
        public Product.Entities.Product Product { get; set; }

        [Required]
        public Store.Entities.Store FromStore { get; set; }

        [Required]
        public Store.Entities.Store ToStore { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public virtual Request Request { get; set; }
    }
}
