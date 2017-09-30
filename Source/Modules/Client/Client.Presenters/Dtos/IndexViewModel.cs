using System.Collections.Generic;

namespace Client.Presenters.Dtos
{
    public class IndexViewModel
    {
        public IEnumerable<Entities.Client> Clients { get; set; }

        public IndexSearchCriteria SearchCriteria { get; set; }
    }
}
