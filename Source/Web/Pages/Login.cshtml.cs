using Administration.Bll;
using Infrastructure.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Web.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required]
        [MaxLength(256)]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        private readonly IAuthenticationServices _authenticationServices;

        public LoginModel(IAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("index");
            }

            var messages = new Messages();
            if (!await _authenticationServices.LoginAsync(Email, Password, messages))
            {
                TempData["Error"] = true;
                TempData["Messages"] = messages.GetWarnings();
                return RedirectToPage("index");
            }

            return RedirectToAction("index", "administration");
        }
    }
}