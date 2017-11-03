using Administration.Bll;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly IAuthenticationServices _authenticationServices;

        public LogoutModel(IAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        public IActionResult OnGet()
        {
            _authenticationServices.Logout();

            TempData["Error"] = true;
            TempData["Messages"] = new string[] { "Logged out" };

            return RedirectToPage("index");
        }
    }
}