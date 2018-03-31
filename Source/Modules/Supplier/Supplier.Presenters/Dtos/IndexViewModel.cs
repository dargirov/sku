using Infrastructure.Data.Common;
using System.Collections.Generic;

namespace Supplier.Presenters.Dtos
{
    public class IndexViewModel
    {
        public (IEnumerable<Entities.Supplier> suppliers, PageData pageData) Suppliers { get; set; }

        public IndexSearchCriteria SearchCriteria { get; set; }
    }
}
