using System;

namespace Request.Presenters.Widgets.PendingRequests
{
    public class RequestDto
    {
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ProductCount { get; set; }
    }
}
