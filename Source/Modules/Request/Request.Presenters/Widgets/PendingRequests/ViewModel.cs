using Infrastructure.Data.Common;
using System.Collections.Generic;

namespace Request.Presenters.Widgets.PendingRequests
{
    public class ViewModel
    {
        public IEnumerable<RequestDto> Requests { get; set; }

        public PageData PageData { get; set; }
    }
}
