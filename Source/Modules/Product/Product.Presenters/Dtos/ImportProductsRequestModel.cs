using Microsoft.AspNetCore.Http;

namespace Product.Presenters.Dtos
{
    public class ImportProductsRequestModel
    {
        public IFormFile File { get; set; }
    }
}
