using Infrastructure.Services.Common.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Client.Presenters.Dtos
{
    public class EditLegalViewModel : IMapFrom<Entities.LegalClient>
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "Mol is required")]
        [MaxLength(100)]
        public string Mol { get; set; }

        [Required(ErrorMessage = "Eik is required")]
        public string Eik { get; set; }

        [Required(ErrorMessage = "Has Dds is required")]
        public bool HasDds { get; set; }

        [Required(ErrorMessage = "Firm name is required")]
        [MaxLength(100)]
        public string FirmName { get; set; }

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
