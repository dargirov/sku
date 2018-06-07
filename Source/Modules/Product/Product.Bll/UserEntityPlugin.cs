using Administration.Bll;
using Administration.Entities;
using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Bll
{
    public class UserEntityPlugin : IUserEntityPlugin
    {
        private readonly IRepository _repository;
        private readonly IEntityServices _entityServices;

        public UserEntityPlugin(IRepository repository, IEntityServices entityServices)
        {
            _repository = repository;
            _entityServices = entityServices;
        }

        public async Task<bool> OnDelete(User user, Messages messages)
        {
            var productPriorities = await _repository.GetQueryable<Entities.ProductPriority>()
                .Where(x => x.User == user)
                .ToListAsync();

            foreach (var productPriority in productPriorities)
            {
                if (!await _entityServices.DeleteAsync<Entities.ProductPriority>(productPriority, messages))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
