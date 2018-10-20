using Infrastructure.Data.Common;
using System.Collections.Generic;

namespace Product.Presenters.Widgets.LowQuantityProducts
{
    public class ViewModel
    {
        public IEnumerable<StoreDto> Stores { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }

        public PageData PageData { get; set; }
    }
}
