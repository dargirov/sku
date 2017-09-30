using Infrastructure.Services.Common.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Client.Presenters.Dtos
{
    public class EditNaturalViewModel : IMapFrom<Entities.NaturalClient>
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "PersonalNo is required")]
        [StringLength(10)]
        public string PersonalNo { get; set; }

        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        public string Email { get; set; }

        public string Comment { get; set; }

        [Required(ErrorMessage = "City is required")]
        public int CityId { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public int CountryId { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }
    }
}
