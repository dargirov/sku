using System;

namespace Product.Presenters.Widgets.LatestProducts
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Category { get; set; }

        public int ManufacturerId { get; set; }

        public string Manufacturer { get; set; }

        public int VariantCount { get; set; }

        public int TotalStockCount { get; set; }
    }
}
