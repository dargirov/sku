using AutoMapper;
using Client.Entities;
using Infrastructure.Services.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Client.Presenters.Dtos
{
    public class EditLegalRequestModel : IMapTo<Entities.LegalClient>, IHaveCustomMappings
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Mol { get; set; }

        [Required]
        [MinLength(3)]
        public string Eik { get; set; }

        [Required]
        public bool HasDds { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string FirmName { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        [MaxLength(1000)]
        public string Comment { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public int CountryId { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<EditLegalRequestModel, LegalClient>()
                .AfterMap((src, dest) => dest.Type = ClientType.Legal);
        }
    }
}
