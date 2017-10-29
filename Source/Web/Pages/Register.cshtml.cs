using Administration.Bll;
using Administration.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Web.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "OrganizationName is required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string OrganizationName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(256)]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        [MinLength(3)]
        [MaxLength(30)]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Confirm password is required")]
        [MinLength(3)]
        [MaxLength(30)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Eik is required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Eik { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Mol is required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Mol { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "FirstName is required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "LastName is required")]
        [MinLength(3)]
        [MaxLength(100)]
        public string LastName { get; set; }

        private IUserServices _userServices;
        private IOrganizationServices _organizationServices;

        public RegisterModel(IUserServices userServices, IOrganizationServices organizationServices)
        {
            _userServices = userServices;
            _organizationServices = organizationServices;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool error = false;
            var errorMessages = new List<string>();

            var user = await _userServices.GetByEmailAsync(Email);
            if (user != null)
            {
                error = true;
                errorMessages.Add("Email address exsists");
            }

            var orgnization = await _organizationServices.GetByNameAsync(OrganizationName);
            if (orgnization != null)
            {
                error = true;
                errorMessages.Add("Organization name exsists");
            }

            if (error)
            {
                TempData["Error"] = true;
                TempData["Messages"] = errorMessages.ToArray();
                return Page();
            }

            var newUser = new User()
            {
                Email = Email,
                Password = Password,
                FirstName = FirstName,
                LastName = LastName,
                IsAdmin = true,
                ModulePrivilege = GetAdminModulePrivilege(),
                Organization = new Organization()
                {
                    Name = OrganizationName,
                    Eik = Eik,
                    Mol = Mol
                }
            };

            //newUser.ModulePrivilege = this.GetAdminModulePrivilege();

            await _userServices.EditAsync(newUser);

            return RedirectToPage("index");
        }

        private ModulePrivilege GetAdminModulePrivilege()
        {
            return new ModulePrivilege()
            {
                StoreRead = true,
                StoreWrite = true,
                StoreDelete = true,
                ManufacturerRead = true,
                ManufacturerWrite = true,
                ManufacturerDelete = true,
                SupplierRead = true,
                SupplierWrite = true,
                SupplierDelete = true,
                ProductRead = true,
                ProductWrite = true,
                ProductDelete = true,
                ProductImport = true,
                CategoryRead = true,
                CategoryWrite = true,
                CategoryDelete = true,
                ClientRead = true,
                ClientWrite = true,
                ClientDelete = true,
                IncomeRead = true,
                IncomeWrite = true,
                IncomeDelete = true,
                InvoiceRead = true,
                InvoiceWrite = true,
                InvoiceDelete = true,
                SaleRead = true,
                SaleWrite = true,
                SaleDelete = true,
            };
        }
    }
}
