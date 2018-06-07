using Administration.Entities;
using Infrastructure.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Store.Entities
{
    public class Store : BaseTenantEntity
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string HashId { get; set; } 

        [Required]
        public int CityId { get; set; }

        public virtual City City { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(200)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string Bank { get; set; }

        [MaxLength(30)]
        public string Iban { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
