using System.ComponentModel.DataAnnotations;

namespace Administration.Presenters.Dtos
{
    public class GlobalSearchRequestModel
    {
        [Required]
        [MinLength(2)]
        public string SearchFor { get; set; }
    }
}
