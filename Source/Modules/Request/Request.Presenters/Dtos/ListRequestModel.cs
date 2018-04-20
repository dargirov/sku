using Infrastructure.Data.Common;

namespace Request.Presenters.Dtos
{
    public class ListRequestModel
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int SortColumn { get; set; }

        public SortDirectionEnum SortDirection { get; set; }
    }
}
