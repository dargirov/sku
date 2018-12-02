using Infrastructure.Services.Common.Mappings;

namespace Product.Presenters.Dtos
{
    public class SettingsViewModel : IMapFrom<Entities.ProductSettings>
    {
        public bool HideMainInfo { get; set; }
    }
}
