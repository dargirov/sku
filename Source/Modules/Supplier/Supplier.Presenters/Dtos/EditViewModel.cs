using Infrastructure.Services.Common.Mappings;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Supplier.Presenters.Dtos
{
    public class EditViewModel : IMapFrom<Entities.Supplier>
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Mol { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Comment { get; set; }

        public string Url { get; set; }
    }
}
