using Administration.Bll;
using Administration.Bll.Dtos;
using Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Store.Bll;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Bll
{
    public class ProductNotificationPlugin : INotificationPlugin
    {
        private readonly IRepository _repository;
        private readonly IStoreServices _storeServices;

        public ProductNotificationPlugin(IRepository repository, IStoreServices storeServices)
        {
            _repository = repository;
            _storeServices = storeServices;
        }

        public async Task<IList<NotificationDto>> GetNotificationsAsync()
        {
            var storeIds = (await _storeServices.GetStoreListWithReadPrivilegeAsync()).Select(x => x.Id);

            var notifications = (await _repository.GetQueryable<Entities.Product, int>()
                .Include(x => x.Variants)
                .ThenInclude(x => x.Stocks)
                .Where(x => x.Variants.Any(v => v.Stocks.Any(s => storeIds.Contains(s.StoreId) && s.Quantity < s.LowQuantity)))
                .ToListAsync())
                .Select(x => new NotificationDto()
                {
                    Text = $"Low quantity for product {x.Name}",
                    Controller = "product",
                    Action = "edit",
                    Id = x.Id
                })
                .ToList();

            return notifications;
        }
    }
}
