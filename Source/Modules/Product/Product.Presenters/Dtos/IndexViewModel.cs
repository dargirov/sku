using Infrastructure.Data.Common;
using System.Collections.Generic;

namespace Product.Presenters.Dtos
{
    public class IndexViewModel
    {
        public (IEnumerable<Entities.Product> products, PageData pageData) Products { get; set; }

        public IndexSearchCriteria SearchCriteria { get; set; }
    }
}
