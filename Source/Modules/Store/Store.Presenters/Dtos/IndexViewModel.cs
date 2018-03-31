using Infrastructure.Data.Common;
using System.Collections.Generic;

namespace Store.Presenters.Dtos
{
    public class IndexViewModel
    {
        public IndexSearchCriteria SearchCriteria { get; set; }
        public (IEnumerable<Entities.Store> stores, PageData pageData) Stores { get; set; }
    }
}
