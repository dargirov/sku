using Infrastructure.Services.Common.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Product.Presenters.Dtos
{
    public class EditViewModel : IMapFrom<Entities.Product>
    {
        [HiddenInput]
        public int Id { get; set; }

        public string Number { get; set; }

        [Required]
        public string Name { get; set; }

        public string Warranty { get; set; }

        public string Description { get; set; }

        public IEnumerable<Store.Entities.Store> Stores { get; set; }

        [Required]
        public string CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        [Required]
        public string ManufacturerId { get; set; }

        public IEnumerable<SelectListItem> Manufacturers { get; set; }

        public string SupplierId { get; set; }

        public IEnumerable<SelectListItem> Suppliers { get; set; }

        public bool IsSaved { get; set; }

        public IEnumerable<Entities.ProductVariant> Variants { get; set; }

        public IEnumerable<Entities.ProductPicture> Pictures { get; set; }

        public bool HideMainInfo { get; set; }

        //public IEnumerable<Entities.StorePrivilege> StorePrivileges { get; set; }
    }
}
