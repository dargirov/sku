using System.Collections.Generic;

namespace Product.Presenters.Dtos
{
    public class CategoriesViewModel
    {
        public IEnumerable<Entities.ProductCategory> Categories { get; set; }
    }
}
