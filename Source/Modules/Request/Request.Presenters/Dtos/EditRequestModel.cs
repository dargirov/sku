using Request.Bll.Dtos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Request.Presenters.Dtos
{
    public class EditRequestModel
    {
        [Required]
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsNextStep { get; set; }

        [Required]
        public IEnumerable<StockRequestDto> StockRequests { get; set; }
    }
}
