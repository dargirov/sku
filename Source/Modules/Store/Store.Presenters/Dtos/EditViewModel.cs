using Infrastructure.Services.Common.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Store.Presenters.Dtos
{
    public class EditViewModel : IMapFrom<Entities.Store>
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "City is required")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        public string Bank { get; set; }

        public string Iban { get; set; }

        public string Description { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }
    }
}
