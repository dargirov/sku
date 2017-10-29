using Administration.Bll.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public interface IGlobalSearchResult
    {
        Task<IList<GlobalSearchResultDto>> GetResults(string search);
    }
}
