using Infrastructure.Data.Common;
using System.Collections.Generic;

namespace Product.Presenters.Widgets.LatestProducts
{
    public class ViewModel
    {
        public IEnumerable<ProductDto> Products { get; set; }

        public PageSortData PageSortData { get; set; }
    }
}
