using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data.Common
{
    public abstract class BaseTenantEntity : BaseEntity
    {
        [Required]
        public Guid TenantId { get; set; }
    }
}
