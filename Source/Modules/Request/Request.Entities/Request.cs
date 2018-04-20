using Infrastructure.Data.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Request.Entities
{
    public class Request : BaseTenantEntity<int>
    {
        public virtual ICollection<StockRequest> StockRequests { get; set; }

        public string Text { get; set; }

        [Required]
        public RequestStatusEnum Status { get; set; }
    }
}
