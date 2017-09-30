using Administration.Bll;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Administration.Presenters
{
    public class AdministrationController : BaseController
    {
        private readonly IUserServices _userServices;

        public AdministrationController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userServices.GetTestAsync(1);
            return View();
        }
    }
}
