using Infrastructure.Data.Common;

namespace Store.Presenters.Dtos
{
    public class IndexRequesModel
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int SortColumn { get; set; }

        public SortDirectionEnum SortDirection { get; set; }

        public IndexSearchCriteria SearchCriteria { get; set; }
    }
}
