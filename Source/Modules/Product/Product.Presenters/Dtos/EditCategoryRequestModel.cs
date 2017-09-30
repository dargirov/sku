using Infrastructure.Services.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Product.Presenters.Dtos
{
    public class EditCategoryRequestModel : IMapTo<Entities.ProductCategory>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
