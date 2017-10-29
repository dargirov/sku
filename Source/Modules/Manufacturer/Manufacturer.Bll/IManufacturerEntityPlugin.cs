using Infrastructure.Data.Common;
using System.Threading.Tasks;

namespace Manufacturer.Bll
{
    public interface IManufacturerEntityPlugin
    {
        Task<bool> OnSave(Entities.Manufacturer manufacturer, Messages messages);
        Task<bool> OnDelete(Entities.Manufacturer manufacturer, Messages messages);
    }
}
