using Administration.Bll;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using Microsoft.EntityFrameworkCore;
using Store.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Bll
{
    public class StoreServices : IStoreServices
    {
        private readonly IRepository _repository;
        private readonly IEntityServices _entityServices;
        private readonly IAuthenticationServices _authenticationServices;

        public StoreServices(IRepository repository, IEntityServices entityServices, IAuthenticationServices authenticationServices)
        {
            _repository = repository;
            _entityServices = entityServices;
            _authenticationServices = authenticationServices;
        }

        public Task<Entities.Store> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync<Entities.Store, int>(id);
        }

        public Task<List<Entities.Store>> GetListAsync()
        {
            return GetListAsync(null, null, null);
        }

        public async Task<List<Entities.Store>> GetListAsync(string name, int? cityId, string address)
        {
            var user = await _authenticationServices.GetCurrentUser();

            var query = _repository.GetQueryable<Entities.Store, int>();

            if (!user.IsAdmin)
            {
                var storeIds = _repository.GetQueryable<StorePrivilege, int>()
                    .Where(x => x.User == user && x.Read)
                    .Select(x => x.StoreId);

                query = query.Where(x => storeIds.Contains(x.Id));
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(s => s.Name.Contains(name));
            }

            if (cityId.HasValue && cityId.Value > 0)
            {
                query = query.Where(s => s.CityId == cityId.Value);
            }

            if (!string.IsNullOrWhiteSpace(address))
            {
                query = query.Where(s => s.Address.Contains(address));
            }

            return await query.ToListAsync();
        }

        public Task<List<Administration.Entities.City>> GetCityListAsync()
        {
            var s = _repository.GetQueryable<Entities.Store, int>()
                .Select(x => x.CityId);

            return _repository.GetQueryable<Administration.Entities.City, int>()
                .Where(x => s.Contains(x.Id))
                .Distinct()
                .ToListAsync();
        }

        public Task<int> EditAsync(Entities.Store store)
        {
            return _entityServices.SaveAsync<Entities.Store, int>(store);
        }

        public Task<List<StorePrivilege>> GetPrivilegeForUserListAsync(int userId)
        {
            return _repository.GetListAsync<StorePrivilege, int>(x => x.UserId == userId);
        }

        public async Task<int> EditPrivilegesAsync(int userId, IEnumerable<StorePrivilege> privileges)
        {
            var currentPrivs = await GetPrivilegeForUserListAsync(userId);

            foreach (var priv in currentPrivs.Where(x => !privileges.Select(y => y.Id).ToList().Contains(x.Id)))
            {
                await _entityServices.DeleteAsync<StorePrivilege, int>(priv);
            }

            foreach (var privilege in privileges)
            {
                privilege.UserId = userId;

                var existingPriv = currentPrivs.FirstOrDefault(x => x.Id == privilege.Id);
                if (existingPriv != null)
                {
                    existingPriv.Read = privilege.Read;
                    existingPriv.Write = privilege.Write;
                    existingPriv.Delete = privilege.Delete;
                }

                await _entityServices.SaveAsync<StorePrivilege, int>(existingPriv ?? privilege);
            }

            return 0;
        }

        public async Task<List<Entities.Store>> GetStoreListWithReadPrivilegeAsync()
        {
            return await GetStoreListWithPrivilegeAsync(x => x.Read);
        }

        public async Task<List<Entities.Store>> GetStoreListWithWritePrivilegeAsync()
        {
            return await GetStoreListWithPrivilegeAsync(x => x.Write);
        }

        public async Task<List<Entities.Store>> GetStoreListWithDeletePrivilegeAsync()
        {
            return await GetStoreListWithPrivilegeAsync(x => x.Delete);
        }

        private async Task<List<Entities.Store>> GetStoreListWithPrivilegeAsync(Func<StorePrivilege, bool> filter)
        {
            var user = await _authenticationServices.GetCurrentUser();

            if (user.IsAdmin)
            {
                return await _repository.GetListAsync<Entities.Store, int>();
            }

            var storeIds = (await _repository.GetListAsync<StorePrivilege, int>(x => x.User == user))
                .Where(filter)
                .Select(x => x.StoreId);

            return await _repository.GetQueryable<Entities.Store, int>()
                .Where(x => storeIds.Contains(x.Id))
                .ToListAsync();
        }
    }
}
