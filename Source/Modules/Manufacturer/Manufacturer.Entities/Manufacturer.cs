using Administration.Entities;
using Infrastructure.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Manufacturer.Entities
{
    public class Manufacturer : BaseTenantEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(1000)]
        public string Comment { get; set; }

        [MaxLength(100)]
        public string Url { get; set; }
    }
}
