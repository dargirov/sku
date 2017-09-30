using System.Collections.Generic;

namespace Product.Presenters.Dtos
{
    public class IndexRequestModel
    {
        public int Start { get; set; }

        public int Length { get; set; }

        public IList<IDictionary<string, string>> Order { get; set; }

        public IndexSearchCriteria SearchCriteria { get; set; }
    }
}
