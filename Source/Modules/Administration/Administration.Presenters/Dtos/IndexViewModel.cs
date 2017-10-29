using Administration.Bll.Dtos;
using System.Collections.Generic;

namespace Administration.Presenters.Dtos
{
    public class IndexViewModel
    {
        public IEnumerable<NotificationDto> Notifications { get; set; }
    }
}
