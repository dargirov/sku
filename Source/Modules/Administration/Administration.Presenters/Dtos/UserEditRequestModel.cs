using AutoMapper;
using Infrastructure.Services.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Administration.Presenters.Dtos
{
    public class UserEditRequestModel : IMapTo<UserEditViewModel>, IMapTo<Entities.User>, IHaveCustomMappings
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(1000)]
        public string Comment { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        [MinLength(5)]
        [MaxLength(256)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [MinLength(5)]
        [MaxLength(256)]
        public string ConfirmPassword { get; set; }

        public bool ChangePassword { get; set; }

        //public ModulePrivilegesRequestModel Privilege { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<UserEditRequestModel, UserEditViewModel > ()
                .AfterMap((src, dest) => dest.IsSaved = src.Id > 0);

            configuration.CreateMap<UserEditRequestModel, Entities.User>()
                .ForSourceMember(x => x.Id, y => y.Ignore());
        }
    }
}
