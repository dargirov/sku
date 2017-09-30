﻿using Infrastructure.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Product.Entities
{
    public class ProductStock : BaseTenantEntity<int>
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public MeasureType QuantityMeasureType { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int LowQuantity { get; set; }

        [Required]
        public MeasureType LowQuantityMeasureType { get; set; }

        [Required]
        public int ProductVariantId { get; set; }

        public virtual ProductVariant Variant { get; set; }

        [Required]
        public int StoreId { get; set; }

        public virtual Store.Entities.Store Store { get; set; }
    }
}
