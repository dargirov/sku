using Microsoft.AspNetCore.Mvc;
using Product.Entities;
using System.Collections.Generic;

namespace Product.Presenters.Dtos
{
    public class PicturesViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        public IEnumerable<ProductPicture> Pictures { get; set; }
    }
}
