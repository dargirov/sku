using System;
using System.Collections.Generic;

namespace Product.Presenters.Widgets.LowQuantityProducts
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> VariantNames { get; set; }

        public IEnumerable<string> StoreNames { get; set; }

        public IEnumerable<int> Quantities { get; set; }

        public IEnumerable<int> LowQuantities { get; set; }

        public IEnumerable<DateTime?> Dates { get; set; }
    }
}
