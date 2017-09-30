using Infrastructure.Data.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Product.Entities
{
    public class Product : BaseTenantEntity<int>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual ProductCategory Category { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        public virtual Manufacturer.Entities.Manufacturer Manufacturer { get; set; }

        public int SupplierId { get; set; }

        public virtual Supplier.Entities.Supplier Supplier { get; set; }

        [MaxLength(20)]
        public string Warranty { get; set; }

        public virtual ICollection<ProductPicture> Pictures { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        //public virtual ICollection<ProductStock> Stocks { get; set; }

        public virtual ICollection<ProductVariant> Variants { get; set; }
    }
}
