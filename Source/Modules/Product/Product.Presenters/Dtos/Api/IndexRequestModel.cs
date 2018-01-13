using System.ComponentModel.DataAnnotations;

namespace Product.Presenters.Dtos.Api
{
    public class IndexRequestModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Hid { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Code { get; set; }
    }
}
