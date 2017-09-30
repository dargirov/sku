using Infrastructure.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Administration.Entities
{
    public class City : BaseEntity<int>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int PostCode { get; set; }

        [Required]
        public CityTypeEnum Type { get; set; }

        [Required]
        public bool Highlight { get; set; }
    }
}
