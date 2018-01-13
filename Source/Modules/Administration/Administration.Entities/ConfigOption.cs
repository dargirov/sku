using Infrastructure.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Administration.Entities
{
    public class ConfigOption : BaseTenantEntity<int>
    {
        [Required]
        public int EntityId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Entity { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public ConfigOptionTypeEnum Type { get; set; }

        [Required]
        public ConfigOptionCategoryEnum Category { get; set; }
    }
}
