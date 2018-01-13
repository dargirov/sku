using Administration.Presenters;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Product.Bll;
using System.Threading.Tasks;

namespace Product.Presenters
{
    [Route("api/product")]
    [EnableCors("AllowAllOrigins")]
    public class ProductApiController : BaseController
    {
        private readonly IProductServices _productServices;

        public ProductApiController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        public async Task<IActionResult> Index(Dtos.Api.IndexRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var productDtos = await _productServices.GetByOrganizationAndVariantCodeAsync(model.Hid, model.Code);
            return Json(productDtos);
        }
    }
}
