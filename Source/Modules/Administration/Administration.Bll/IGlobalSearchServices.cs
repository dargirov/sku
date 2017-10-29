using Administration.Bll.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public interface IGlobalSearchServices
    {
        Task<IList<GlobalSearchResultDto>> GetResults(string search);
    }
}
