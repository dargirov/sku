using AutoMapper;
using Infrastructure.Services.Common.Mappings;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Administration.Presenters.Dtos
{
    public class UserEditViewModel : IMapFrom<Entities.User>, IHaveCustomMappings
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm passwords is required")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        public string Address { get; set; }

        public string Comment { get; set; }

        public bool IsSaved { get; set; }

        //public ModulePrivilege Privilege { get; set; }

        public bool IsAdmin { get; set; }

        //public IEnumerable<Entities.Store> AllStores { get; set; }

        public bool ChangePassword { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //configuration.CreateMap<User, EditViewModel>()
            //    .AfterMap((src, dest) => dest.IsSaved = src.Id > 0);
            // make sure to hide the pasword
            //configuration.CreateMap<Data.Models.User, EditViewModel>()
            //    .ForMember(x => x.Password, opt => opt.Ignore());
            //configuration.CreateMap<Data.Models.User, EditViewModel>()
            //    .ForMember(x => x.ConfirmPassword, opt => opt.UseValue<string>(string.Empty));
        }
    }
}
