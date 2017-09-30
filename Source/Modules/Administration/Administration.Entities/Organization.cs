using Infrastructure.Data.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Administration.Entities
{
    public class Organization : BaseTenantEntity<int>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Mol { get; set; }

        [Required]
        [MaxLength(100)]
        public string Eik { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
