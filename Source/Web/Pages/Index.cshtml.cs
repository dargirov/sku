using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sku.Web.Pages
{
    public class IndexModel : PageModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public void OnGet()
        {

        }
    }
}
