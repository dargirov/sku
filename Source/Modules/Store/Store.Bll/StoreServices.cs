using Administration.Bll;
using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using Microsoft.EntityFrameworkCore;
using Store.Entities;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Bll
{
    public class StoreServices : IStoreServices
    {
        private readonly IContainer _container;
        private readonly IRepository _repository;
        private readonly IEntityServices _entityServices;
        private readonly IAuthenticationServices _authenticationServices;
        private readonly IHashServices _hashServices;

        public StoreServices(IContainer container, IRepository repository, IEntityServices entityServices, IAuthenticationServices authenticationServices, IHashServices hashServices)
        {
            _container = container;
            _repository = repository;
            _entityServices = entityServices;
            _authenticationServices = authenticationServices;
            _hashServices = hashServices;
        }

        public Task<Entities.Store> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync<Entities.Store>(id);
        }

        public async Task<List<Entities.Store>> GetListAsync()
        {
            var user = await _authenticationServices.GetCurrentUserAsync();
            var query = _repository.GetQueryable<Entities.Store>();

            if (!user.IsAdmin)
            {
                var storeIds = _repository.GetQueryable<StorePrivilege>()
                    .Where(x => x.User == user && x.Read)
                    .Select(x => x.StoreId);

                query = query.Where(x => storeIds.Contains(x.Id));
            }

            return await query.ToListAsync();
        }

        public Task<List<Entities.Store>> GetListWithoutPrivCheckAsync()
        {
            return _repository.GetListAsync<Entities.Store>();
        }

        public async Task<(IEnumerable<Entities.Store> stores, PageData pageData)> GetListAsync(int page, int pageSize, int column, SortDirectionEnum dir, string name, int? cityId, string address)
        {
            var user = await _authenticationServices.GetCurrentUserAsync();
            var query = _repository.GetQueryable<Entities.Store>();

            if (!user.IsAdmin)
            {
                var storeIds = _repository.GetQueryable<StorePrivilege>()
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

            return await query.ToListWithPageData(page, pageSize);
        }

        public Task<List<Administration.Entities.City>> GetCityListAsync()
        {
            var s = _repository.GetQueryable<Entities.Store>()
                .Select(x => x.CityId);

            return _repository.GetQueryable<Administration.Entities.City>()
                .Where(x => s.Contains(x.Id))
                .Distinct()
                .ToListAsync();
        }

        public async Task<bool> EditAsync(Entities.Store store, Messages messages)
        {
            var user = await _authenticationServices.GetCurrentUserAsync();
            var generateHashId = false;

            if (!store.IsSaved)
            {
                generateHashId = true;
                store.HashId = string.Empty;
                var storePriv = new StorePrivilege()
                {
                    Read = true,
                    Write = true,
                    Delete = true,
                    Store = store,
                    User = user
                };

                if (!await _entityServices.SaveAsync<StorePrivilege>(storePriv, messages))
                {
                    return false;
                }
            }

            var result = await _entityServices.SaveAsync<Entities.Store>(store, messages);
            if (!result)
            {
                return false;
            }

            if (generateHashId)
            {
                store.HashId = _hashServices.Base36Encode(100000 + store.Id);
                await _entityServices.SaveAsync<Entities.Store>(store, messages);
            }

            return result;
        }

        public Task<List<StorePrivilege>> GetPrivilegeForUserListAsync(int userId)
        {
            return _repository.GetListAsync<StorePrivilege>(x => x.UserId == userId);
        }

        public async Task<bool> EditPrivilegesAsync(int userId, IEnumerable<StorePrivilege> privileges, Messages messages)
        {
            var currentPrivs = await GetPrivilegeForUserListAsync(userId);

            foreach (var priv in currentPrivs.Where(x => !privileges.Select(y => y.Id).ToList().Contains(x.Id)))
            {
                if (!await _entityServices.DeleteAsync<StorePrivilege>(priv, messages))
                {
                    return false;
                }
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

                if (!await _entityServices.SaveAsync<StorePrivilege>(existingPriv ?? privilege, messages))
                {
                    return false;
                }
            }

            return true;
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

        public async Task<bool> DeleteAsync(Entities.Store store, Messages messages)
        {
            foreach (var plugin in _container.GetAllInstances<IStoreEntityPlugin>())
            {
                if (!await plugin.OnDelete(store, messages))
                {
                    return false;
                }
            }

            return await _entityServices.DeleteAsync<Entities.Store>(store, messages);
        }

        private async Task<List<Entities.Store>> GetStoreListWithPrivilegeAsync(Func<StorePrivilege, bool> filter)
        {
            var user = await _authenticationServices.GetCurrentUserAsync();

            if (user.IsAdmin)
            {
                return await _repository.GetListAsync<Entities.Store>();
            }

            var storeIds = (await _repository.GetListAsync<StorePrivilege>(x => x.User == user))
                .Where(filter)
                .Select(x => x.StoreId);

            return await _repository.GetQueryable<Entities.Store>()
                .Where(x => storeIds.Contains(x.Id))
                .ToListAsync();
        }
    }
}
