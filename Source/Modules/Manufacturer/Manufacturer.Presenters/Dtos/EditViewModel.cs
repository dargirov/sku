using Infrastructure.Services.Common.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Manufacturer.Presenters.Dtos
{
    public class EditViewModel : IMapFrom<Entities.Manufacturer>
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int CountryId { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }

        public string Email { get; set; }

        public string Comment { get; set; }

        public string Url { get; set; }
    }
}
