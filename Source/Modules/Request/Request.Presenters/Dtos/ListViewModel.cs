using Infrastructure.Data.Common;
using System.Collections.Generic;

namespace Request.Presenters.Dtos
{
    public class ListViewModel
    {
        public (IEnumerable<Entities.Request> requests, PageData pageData) Requests { get; set; }
    }
}
