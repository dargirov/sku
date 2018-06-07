using Administration.Bll;
using Administration.Entities;
using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Bll
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
            var storePrivileges = await _repository.GetQueryable<Entities.StorePrivilege>()
                .Where(x => x.User == user)
                .ToListAsync();

            foreach (var storePrivilege in storePrivileges)
            {
                if (!await _entityServices.DeleteAsync<Entities.StorePrivilege>(storePrivilege, messages))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
