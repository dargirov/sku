using Infrastructure.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Store.Entities
{
    public class StorePrivilege : BaseTenantEntity
    {
        [Required]
        public bool Read { get; set; }

        [Required]
        public bool Write { get; set; }

        [Required]
        public bool Delete { get; set; }

        [Required]
        public int StoreId { get; set; }

        public virtual Store Store { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual Administration.Entities.User User { get; set; }
    }
}
