using Infrastructure.Data.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Product.Entities;
using System.Collections.Generic;

namespace Request.Presenters.Dtos
{
    public class IndexViewModel
    {
        public (IEnumerable<Product.Entities.Product> products, PageData pageData) Products { get; set; }

        public int StoreId { get; set; }

        public IEnumerable<(int storeId, int quantity)> StoreQuantities { get; set; }

        public IEnumerable<Entities.Request> NewRequests { get; set; }

        public IEnumerable<SelectListItem> Stores { get; set; }

        public IndexSearchCriteria SearchCriteria { get; set; }

        public Dictionary<int, ProductPriorityEnum> ProductsPriority { get; set; }
    }
}
