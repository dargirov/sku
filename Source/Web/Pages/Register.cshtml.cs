using Administration.Bll;
using Administration.Entities;
using Infrastructure.Services.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        private readonly IUserServices _userServices;
        private readonly IOrganizationServices _organizationServices;
        private readonly ICacheServices _cacheServices;
        private readonly IHashServices _hashServices;

        public RegisterModel(IUserServices userServices, IOrganizationServices organizationServices, ICacheServices cacheServices, IHashServices hashServices)
        {
            _userServices = userServices;
            _organizationServices = organizationServices;
            _cacheServices = cacheServices;
            _hashServices = hashServices;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            Random random = new Random();
            //var p = _hashServices.Base36Encode(100000 + random.Next());

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
                    HashId = _hashServices.Base36Encode(100000 + random.Next()),
                    Mol = Mol
                }
            };

            _cacheServices.Set<string>("tenant_id", Guid.NewGuid().ToString());

            await _userServices.EditAsync(newUser);

            return RedirectToPage("index");
        }

        private ModulePrivilege GetAdminModulePrivilege()
        {
            var privileges = new ModulePrivilege();

            foreach (var prop in privileges.GetType().GetProperties().Where(x => x.PropertyType == typeof(bool) && !x.Name.StartsWith("Is")))
            {
                var setter = prop.GetSetMethod();
                if (setter == null)
                {
                    continue;
                }

                prop.SetValue(privileges, true);
            }

            return privileges;
        }
    }
}
