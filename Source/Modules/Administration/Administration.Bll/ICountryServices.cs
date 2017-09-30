using System.Collections.Generic;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public interface ICountryServices
    {
        Task<List<Entities.Country>> GetListAsync();
    }
}
