using Infrastructure.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Administration.Entities
{
    public class PageData : BaseTenantEntity
    {
        [Required]
        public User User { get; set; }

        [Required]
        [MaxLength(100)]
        public string GridId { get; set; }

        [Required]
        public int PageSize { get; set; }
    }
}
