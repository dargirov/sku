using Administration.Entities;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public class UserServices : IUserServices
    {
        private readonly IRepository _repository;
        public readonly IEntityServices _entityServices;

        public UserServices(IRepository repository, IEntityServices entityServices)
        {
            _repository = repository;
            _entityServices = entityServices;
        }

        public Task<User> GetByIdAsync(int id)
        {
            return _repository.GetQueryable<User, int>()
                .Include(x => x.ModulePrivilege)
                .Include(x => x.Organization)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<User> GetByEmailAsync(string email)
        {
            return _repository.GetQueryable<User, int>()
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

        public Task<int> EditAsync(User user)
        {
            if (!user.IsSaved)
            {
                user.Password = Password.HashPassword(user.Password);
            }

            return _entityServices.SaveAsync<User, int>(user);
        }

        public Task<int> EditAsync(User user, bool hashPassword)
        {
            if (hashPassword)
            {
                user.Password = Password.HashPassword(user.Password);
            }

            return _entityServices.SaveAsync<User, int>(user);
        }

        public Task<List<User>> GetListAsync()
        {
            return _repository.GetListAsync<User, int>();
        }
    }
}
