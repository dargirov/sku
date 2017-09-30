using Infrastructure.Services.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Supplier.Presenters.Dtos
{
    public class EditRequestModel  : IMapTo<Entities.Supplier>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Mol { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(1000)]
        public string Comment { get; set; }

        [MaxLength(100)]
        public string Url { get; set; }
    }
}
