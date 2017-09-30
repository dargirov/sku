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
    }
}
