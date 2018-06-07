using Administration.Entities;
using Infrastructure.Data.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.Entities
{
    public abstract class Client : BaseTenantEntity
    {
        [MaxLength(50)]
        public string Phone { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(1000)]
        public string Comment { get; set; }

        [Required]
        [NotMapped]
        public ClientTypeEnum Type { get; set; }

        [Required]
        public int CityId { get; set; }

        public virtual City City { get; set; }

        [Required]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
