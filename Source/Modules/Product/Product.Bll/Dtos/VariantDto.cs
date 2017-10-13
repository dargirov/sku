using AutoMapper;
using Infrastructure.Services.Common.Mappings;
using Product.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Product.Bll.Dtos
{
    public class VariantDto : IMapTo<ProductVariant>, IHaveCustomMappings
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Code { get; set; }

        [Required]
        public decimal PriceNet { get; set; }

        [Required]
        public CurrencyTypeEnum PriceNetType { get; set; }

        [Required]
        public decimal PriceCustomer { get; set; }

        [Required]
        public CurrencyTypeEnum PriceCustomerType { get; set; }

        [Required]
        public int ProductId { get; set; }

        public IEnumerable<StockDto> Stocks { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<VariantDto, ProductVariant>()
                .ForMember(x => x.Stocks, x => x.Ignore());
        }
    }
}
