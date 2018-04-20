using Infrastructure.Data.Common;
using Product.Entities;
using System.ComponentModel.DataAnnotations;

namespace Request.Entities
{
    public class StockRequest : BaseTenantEntity<int>
    {
        [Required]
        public int StockId { get; set; }

        public ProductStock Stock { get; set; }

        [Required]
        public int FromStoreId { get; set; }

        public Store.Entities.Store FromStore { get; set; }

        [Required]
        public int ToStoreId { get; set; }

        public Store.Entities.Store ToStore { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public virtual Request Request { get; set; }

        [Required]
        public ProductPriorityEnum Priority { get; set; }
    }
}
