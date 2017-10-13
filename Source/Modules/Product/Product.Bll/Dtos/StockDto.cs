using Infrastructure.Services.Common.Mappings;
using Product.Entities;
using System.ComponentModel.DataAnnotations;

namespace Product.Bll.Dtos
{
    public class StockDto : IMapTo<ProductStock>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public MeasureTypeEnum QuantityMeasureType { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int LowQuantity { get; set; }

        [Required]
        public MeasureTypeEnum LowQuantityMeasureType { get; set; }

        [Required]
        public int StoreId { get; set; }
    }
}
