using Request.Bll.Dtos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Request.Presenters.Dtos
{
    public class IndexCreateRequestModel
    {
        [Required]
        public IEnumerable<StockRequestDto> StockRequests { get; set; }

        [Required]
        public int RequestId { get; set; }

        public bool CreateNewRequest => RequestId == 0;
    }
}
