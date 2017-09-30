using System.Collections.Generic;

namespace Product.Presenters.Dtos
{
    public class IndexViewModel
    {
        public IEnumerable<Entities.Product> Products { get; set; }

        public IndexSearchCriteria SearchCriteria { get; set; }
    }
}
