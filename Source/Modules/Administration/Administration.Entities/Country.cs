using Infrastructure.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Administration.Entities
{
    public class Country : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string NameEn { get; set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        [Required]
        public bool Highlight { get; set; }
    }
}
