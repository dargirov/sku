using System.ComponentModel.DataAnnotations;

namespace Store.Presenters.Dtos
{
    public class ConfigRequestModel
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Color { get; set; }
    }
}
