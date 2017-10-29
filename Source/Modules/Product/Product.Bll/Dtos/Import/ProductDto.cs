using System.Collections.Generic;

namespace Product.Bll.Dtos.Import
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public ManufacturerDto Manufacturer { get; set; }
        public IList<VariantDto> Variants { get; set; }
    }
}
