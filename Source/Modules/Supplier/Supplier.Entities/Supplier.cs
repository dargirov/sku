using Infrastructure.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Supplier.Entities
{
    public class Supplier : BaseTenantEntity
    {
        [Required]
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

        public override string ToString()
        {
            return $"{Name} - {Id}";
        }
    }
}
