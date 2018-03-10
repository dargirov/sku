using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Product.Presenters.Dtos
{
    public class IndexSearchCriteria
    {
        public string Name { get; set; }

        public int StoreId { get; set; }

        public IEnumerable<SelectListItem> Stores { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public int ManufacturerId { get; set; }

        public IEnumerable<SelectListItem> Manufacturers { get; set; }

        public int SupplierId { get; set; }

        public IEnumerable<SelectListItem> Suppliers { get; set; }

        public string Description { get; set; }
    }
}
