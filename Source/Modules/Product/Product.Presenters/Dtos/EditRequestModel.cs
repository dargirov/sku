using AutoMapper;
using Infrastructure.Services.Common.Mappings;
using Microsoft.AspNetCore.Http;
using Product.Bll.Dtos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Product.Presenters.Dtos
{
    public class EditRequestModel : IMapTo<Entities.Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        public int? SupplierId { get; set; }

        [MaxLength(20)]
        public string Warranty { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [IgnoreMap]
        public ICollection<IFormFile> FormFiles { get; set; }

        [Required]
        [IgnoreMap]
        public IEnumerable<VariantDto> Variants { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<EditRequestModel, Entities.Product>()
                .ForMember(x => x.Variants, x => x.Ignore());
        }
    }
}
