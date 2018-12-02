using Administration.Bll;
using Administration.Presenters.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Administration.Presenters
{
    [Authorize(Policy = "LoggedIn")]
    public class GlobalSearchController : BaseController
    {
        private readonly IGlobalSearchServices _globalSearchServices;

        public GlobalSearchController(IGlobalSearchServices globalSearchServices)
        {
            _globalSearchServices = globalSearchServices;
        }

        public async Task<IActionResult> Index(GlobalSearchRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { });
            }

            var results = await _globalSearchServices.GetResults(model.SearchFor.Trim());
            var viewModel = new GlobalSearchViewModel()
            {
                Count = results.Count,
                Results = results
            };

            return Json(viewModel);
        }
    }
}
