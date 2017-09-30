using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Client.Presenters.Dtos
{
    public class IndexSearchCriteria
    {
        public string MolName { get; set; }

        public string FirmaNamePersonlNo { get; set; }

        public int CityId { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }
    }
}
