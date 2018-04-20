using Infrastructure.Data.Common;

namespace Administration.Presenters.Dtos
{
    public class PagerViewModel
    {
        public int PageOffset => 4;
        public string Action { get; set; }
        public PageData PageData { get; set; }
    }
}
