using Product.Entities;
using System.ComponentModel.DataAnnotations;

namespace Request.Bll.Dtos
{
    public class StockRequestDto
    {
        [Required]
        public int StockId { get; set; }

        [Required]
        public int FromStoreId { get; set; }

        [Required]
        public int ToStoreId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public ProductPriorityEnum Priority { get; set; }
    }
}
