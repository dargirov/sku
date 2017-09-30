using Infrastructure.Services.Common.Mappings;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Product.Presenters.Dtos
{
    public class EditCategoryViewModel : IMapFrom<Entities.ProductCategory>
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
