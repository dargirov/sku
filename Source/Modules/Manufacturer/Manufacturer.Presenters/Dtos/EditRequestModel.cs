using Infrastructure.Services.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Manufacturer.Presenters.Dtos
{
    public class EditRequestModel : IMapTo<Entities.Manufacturer>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int CountryId { get; set; }

        public string Email { get; set; }

        public string Url { get; set; }

        public string Comment { get; set; }
    }
}
