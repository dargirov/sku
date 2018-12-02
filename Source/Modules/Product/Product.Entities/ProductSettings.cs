using Administration.Entities;
using Infrastructure.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Product.Entities
{
    public class ProductSettings : BaseTenantEntity
    {
        [Required]
        public bool HideMainInfo { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
