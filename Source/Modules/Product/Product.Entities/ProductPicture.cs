using Infrastructure.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Product.Entities
{
    public class ProductPicture : BaseTenantEntity<int>
    {
        [Required]
        public int FullSizeId { get; set; }

        public Administration.Entities.File FullSize { get; set; }

        [Required]
        public int ThumbId { get; set; }

        public Administration.Entities.File Thumb { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
