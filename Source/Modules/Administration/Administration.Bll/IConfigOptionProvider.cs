using Administration.Bll.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public interface IConfigOptionProvider
    {
        Task<IList<ConfigOptionDto>> GetApiOptions();
    }
}
