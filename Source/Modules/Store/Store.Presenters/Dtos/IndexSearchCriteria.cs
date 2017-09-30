using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Store.Presenters.Dtos
{
    public class IndexSearchCriteria
    {
        public string Name { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

        public int CityId { get; set; }

        public string Address { get; set; }
    }
}
