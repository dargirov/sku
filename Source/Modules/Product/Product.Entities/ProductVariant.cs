using Infrastructure.Data.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Utils;

namespace Product.Entities
{
    public class ProductVariant : BaseTenantEntity
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Code { get; set; }

        [Required]
        public decimal PriceNet { get; set; }

        [Required]
        public CurrencyTypeEnum PriceNetType { get; set; }

        [NotMapped]
        public decimal PriceGross => (PriceNet * 1.2m).RoundFinance(2);

        [Required]
        public decimal PriceCustomer { get; set; }

        [Required]
        public CurrencyTypeEnum PriceCustomerType { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public virtual ICollection<ProductStock> Stocks { get; set; }
    }
}
