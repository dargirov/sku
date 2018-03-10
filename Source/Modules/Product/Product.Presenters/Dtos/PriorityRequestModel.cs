using System.ComponentModel.DataAnnotations;

namespace Product.Presenters.Dtos
{
    public class PriorityRequestModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Priority { get; set; }
    }
}
