using Infrastructure.Data.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Request.Entites
{
    public class Request : BaseTenantEntity<int>
    {
        public ICollection<ProductRequest> ProductRequests { get; set; }

        public string Text { get; set; }

        [Required]
        public RequestStatusEnum Status { get; set; }
    }
}
