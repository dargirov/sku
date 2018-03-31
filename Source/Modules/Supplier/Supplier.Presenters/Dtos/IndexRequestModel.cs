using Infrastructure.Data.Common;

namespace Supplier.Presenters.Dtos
{
    public class IndexRequestModel
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int SortColumn { get; set; }

        public SortDirectionEnum SortDirection { get; set; }

        public IndexSearchCriteria SearchCriteria { get; set; }
    }
}
