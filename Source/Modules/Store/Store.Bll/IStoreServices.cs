using Store.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Bll
{
    public interface IStoreServices
    {
        Task<Entities.Store> GetByIdAsync(int id);
        Task<List<Entities.Store>> GetListAsync();
        Task<List<Entities.Store>> GetListAsync(string name, int? cityId, string address);
        Task<List<Administration.Entities.City>> GetCityListAsync();
        Task<int> EditAsync(Entities.Store store);
        Task<List<StorePrivilege>> GetPrivilegeForUserListAsync(int userId);
        Task<int> EditPrivilegesAsync(int userId, IEnumerable<StorePrivilege> privileges);
        Task<List<Entities.Store>> GetStoreListWithReadPrivilegeAsync();
        Task<List<Entities.Store>> GetStoreListWithWritePrivilegeAsync();
        Task<List<Entities.Store>> GetStoreListWithDeletePrivilegeAsync();
    }
}
