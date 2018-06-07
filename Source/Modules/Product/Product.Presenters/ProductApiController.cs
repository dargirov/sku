using Administration.Presenters;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Product.Bll;
using System.Threading.Tasks;

namespace Product.Presenters
{
    [EnableCors("AllowAllOrigins")]
    public class ProductApiController : BaseController
    {
        private readonly IProductServices _productServices;

        public ProductApiController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [Route("api/product")]
        public async Task<IActionResult> Index(Dtos.Api.IndexRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var productDtos = await _productServices.GetByOrganizationAndVariantCodeAsync(model.Hid, model.Code);
            return Json(productDtos);
        }

        [Route("api/products")]
        public async Task<IActionResult> Index(string hid)
        {
            if (string.IsNullOrWhiteSpace(hid) || hid.Length < 3 || hid.Length > 20)
            {
                return BadRequest();
            }

            var productDtos = await _productServices.GetByOrganization(hid);
            return Json(productDtos);
        }
    }
}
