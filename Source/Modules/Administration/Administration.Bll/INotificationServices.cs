using Administration.Bll.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public interface INotificationServices
    {
        Task<IList<NotificationDto>> GetNotificationsAsync();
    }
}
