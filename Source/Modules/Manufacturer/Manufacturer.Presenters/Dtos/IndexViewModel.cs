using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Manufacturer.Presenters.Dtos
{
    public class IndexViewModel
    {
        public IEnumerable<Entities.Manufacturer> Manufacturers { get; set; }

        public IndexSearchCriteria SearchCriteria { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }
    }
}
