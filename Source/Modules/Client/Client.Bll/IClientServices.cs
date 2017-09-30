using Client.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Bll
{
    public interface IClientServices
    {
        Task<Entities.Client> GetByIdAsync(int id, ClientType type);
        Task<IEnumerable<Entities.Client>> GetListAsync(string molName, string firmNamePersonalNo, int? cityId, string address, string phone, string email);
        Task<int> EditAsync(Entities.Client client);
    }
}
