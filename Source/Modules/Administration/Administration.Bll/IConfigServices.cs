using Administration.Bll.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public interface IConfigServices
    {
        Task<IList<ConfigOptionDto>> GetListAsync(Entities.ConfigOptionCategoryEnum category);
        Task<int> EditAsync(IEnumerable<ConfigOptionDto> configOptionDtos);
    }
}
