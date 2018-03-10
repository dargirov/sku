using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Product.Entities;
using System.Collections.Generic;

namespace Product.Presenters.Dtos
{
    public class PriorityViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        public int PicturesCount { get; set; }

        public ProductPriority Priority { get; set; }

        public IEnumerable<ProductPriority> Priorities { get; set; }

        public IEnumerable<SelectListItem> PriorityItems { get; set; }
    }
}
