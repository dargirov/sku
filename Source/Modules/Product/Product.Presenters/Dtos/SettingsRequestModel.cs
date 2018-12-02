using Infrastructure.Services.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Product.Presenters.Dtos
{
    public class SettingsRequestModel : IMapTo<Entities.ProductSettings>
    {
        [Required]
        public bool HideMainInfo { get; set; }
    }
}
