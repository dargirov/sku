using Administration.Bll.Dtos;
using Infrastructure.Data.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public interface IConfigServices
    {
        Task<IList<ConfigOptionDto>> GetListAsync(Entities.ConfigOptionCategoryEnum category);
        Task<bool> EditAsync(IEnumerable<ConfigOptionDto> configOptionDtos, Messages messages);
    }
}
