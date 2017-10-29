using System.ComponentModel.DataAnnotations;

namespace Administration.Presenters.Dtos
{
    public class GlobalSearchRequestModel
    {
        [Required]
        [MinLength(3)]
        public string SearchFor { get; set; }
    }
}
