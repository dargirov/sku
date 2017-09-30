using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data.Common
{
    public abstract class BaseTenantEntity<T> : BaseEntity<T>
    {
        [Required]
        public Guid TenantId { get; set; }
    }
}
