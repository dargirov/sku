using Administration.Bll.Dtos;
using StructureMap;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public class NotificationServices : INotificationServices
    {
        private readonly IContainer _container;

        public NotificationServices(IContainer container)
        {
            _container = container;
        }

        public async Task<IList<NotificationDto>> GetNotificationsAsync()
        {
            var result = new List<NotificationDto>();

            foreach (var plugin in _container.GetAllInstances<INotificationPlugin>())
            {
                result.AddRange(await plugin.GetNotificationsAsync());
            }

            return result;
        }
    }
}
