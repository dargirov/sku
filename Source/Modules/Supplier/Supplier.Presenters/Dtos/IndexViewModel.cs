using System.Collections.Generic;

namespace Supplier.Presenters.Dtos
{
    public class IndexViewModel
    {
        public IEnumerable<Entities.Supplier> Suppliers { get; set; }

        public IndexSearchCriteria SearchCriteria { get; set; }
    }
}
