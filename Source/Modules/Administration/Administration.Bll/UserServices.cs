using Administration.Entities;
using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using Microsoft.EntityFrameworkCore;
using StructureMap;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public class UserServices : IUserServices
    {
        private readonly IContainer _container;
        private readonly IRepository _repository;
        public readonly IEntityServices _entityServices;

        public UserServices(IContainer container, IRepository repository, IEntityServices entityServices)
        {
            _container = container;
            _repository = repository;
            _entityServices = entityServices;
        }

        public Task<User> GetByIdAsync(int id)
        {
            return _repository.GetQueryable<User>()
                .Include(x => x.ModulePrivilege)
                .Include(x => x.Organization)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<User> GetByEmailAsync(string email)
        {
            return _repository.GetQueryable<User>(ignoreQueryFilters: true)
                .Include(x => x.ModulePrivilege)
                .Include(x => x.Organization)
                .Where(x => x.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetByEmailAndPasswordAsync(string email, string password)
        {
            var user = await GetByEmailAsync(email);
            if (user == null)
            {
                return null;
            }

            return Password.VerifyPassword(user.Password, password) ? user : null;
        }

        public Task<bool> EditAsync(User user, Messages messages)
        {
            if (!user.IsSaved)
            {
                user.Password = Password.HashPassword(user.Password);
            }

            return _entityServices.SaveAsync<User>(user, messages);
        }

        public Task<bool> EditAsync(User user, bool hashPassword, Messages messages)
        {
            if (hashPassword)
            {
                user.Password = Password.HashPassword(user.Password);
            }

            return _entityServices.SaveAsync<User>(user, messages);
        }

        public Task<List<User>> GetListAsync()
        {
            return _repository.GetListAsync<User>();
        }

        public async Task<bool> DeleteAsync(User user, Messages messages)
        {
            using (var transaction = await _repository.BeginTransactionAsync())
            {
                var modulePrivilegeCount = await _repository.GetQueryable<User>().Where(x => x.ModulePrivilege.Id == user.ModulePrivilege.Id).CountAsync();
                if (modulePrivilegeCount == 1)
                {
                    if (!await _entityServices.DeleteAsync<ModulePrivilege>(user.ModulePrivilege, messages))
                    {
                        return false;
                    }
                }

                if (!await _entityServices.DeleteAsync<User>(user, messages))
                {
                    return false;
                }

                transaction.Commit();
            }

            return true;
        }
    }
}
