using System.Collections.Generic;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public interface ICityServices
    {
        Task<List<Entities.City>> GetListAsync();
    }
}
