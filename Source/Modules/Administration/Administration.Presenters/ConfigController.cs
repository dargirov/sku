using Administration.Bll;
using Administration.Presenters.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Administration.Presenters
{
    public class ConfigController : BaseController
    {
        private const string _viewsPath = "~/Views/Administration/";
        private readonly IConfigServices _configServices;
        private readonly IAuthenticationServices _authenticationServices;

        public ConfigController(IConfigServices configServices, IAuthenticationServices authenticationServices)
        {
            _configServices = configServices;
            _authenticationServices = authenticationServices;
        }

        public IActionResult Index()
        {
            return View(SetupViewPath("ConfigIndex"));
        }

        public async Task<IActionResult> Api()
        {
            // TODO: maybe use AllowSpecificOrigins cors instead of allowed ip address?
            var user = await _authenticationServices.GetCurrentUserAsync();

            var viewModel = new ConfigApiViewModel()
            {
                ConfigOptions = await _configServices.GetListAsync(Entities.ConfigOptionCategoryEnum.Api),
                OrganizationHash = user.Organization.HashId
            };

            return View(SetupViewPath("ConfigApi"), viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Api(ConfigApiRequestModel model)
        {
            // TODO: fix when IP Address is not set!
            await _configServices.EditAsync(model.ConfigOptions);
            Messages.AddSuccess("Api Edited");
            return RedirectToAction(nameof(Api));
        }

        private string SetupViewPath(string viewName) => $"{_viewsPath}{viewName}.cshtml";
    }
}
