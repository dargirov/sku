using Administration.Bll;
using Administration.Presenters.Dtos;
using Infrastructure.Services.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Administration.Presenters
{
    [Authorize(Policy = "LoggedIn")]
    public class AdministrationController : BaseController
    {
        private readonly IUserServices _userServices;
        private readonly ICacheServices _cacheServices;
        private readonly INotificationServices _notificationServices;

        public AdministrationController(IUserServices userServices, ICacheServices cacheServices, INotificationServices notificationServices)
        {
            _userServices = userServices;
            _cacheServices = cacheServices;
            _notificationServices = notificationServices;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel()
            {
                Notifications = await _notificationServices.GetNotificationsAsync()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Users()
        {
            var users = await _userServices.GetListAsync();
            var viewModel = new UsersViewModel()
            {
                Users = users
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UserEdit(int id)
        {
            if (id == 0)
            {
                return View(new UserEditViewModel());
            }

            var user = await _userServices.GetByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }

            var viewModel = Mapper.Map<UserEditViewModel>(user);
            //viewModel.AllStores = await this.storesService.GetAllAsync();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEdit(UserEditRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.GetErrors().ForEach(x => Messages.AddWarning(x));
                var viewModel = Mapper.Map<UserEditViewModel>(model);
                return View(viewModel);
            }

            var user = await _userServices.GetByEmailAsync(model.Email);
            if (user != null && user.Id != model.Id)
            {
                Messages.AddWarning("Email address exists");
                var viewModel = Mapper.Map<UserEditViewModel>(model);
                return View(viewModel);
            }

            if (user == null && model.Password.Length < 3)
            {
                Messages.AddWarning("The field Password must be a string type with a minimum length of '3'");
                var viewModel = Mapper.Map<UserEditViewModel>(model);
                return View(viewModel);
            }

            if (user == null)
            {
                user = new Entities.User();
                user.OrganizationId = _cacheServices.Get<int>("organization_id");
                user.ModulePrivilege = new Entities.ModulePrivilege()
                {
                    StoreRead = true,
                    ManufacturerRead = true,
                    SupplierRead = true,
                    ProductRead = true,
                    ProductWrite = true,
                    CategoryRead = true,
                    ClientRead = true,
                    IncomeRead = true,
                    InvoiceRead = true,
                    SaleRead = true,
                };
                model.ChangePassword = true;
            }

            var initialPassword = user.Password;
            Mapper.Map(model, user);

            if (!model.ChangePassword && initialPassword != null)
            {
                user.Password = initialPassword;
            }

            await _userServices.EditAsync(user, hashPassword: model.ChangePassword);
            Messages.AddSuccess("Store Edited");

            return RedirectToAction(nameof(UserEdit), new { id = user.Id });
        }

        [HttpGet]
        public async Task<IActionResult> ModulePrivileges(int id)
        {
            var user = await _userServices.GetByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }

            if (user.IsAdmin)
            {
                return RedirectToAction(nameof(UserEdit), new { id = id });
            }

            var viewModel = Mapper.Map<ModulePrivilegesViewModel>(user.ModulePrivilege);
            viewModel.UserId = id;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ModulePrivileges(ModulePrivilegesRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userServices.GetByIdAsync(model.Id);
            if (user == null || user.IsAdmin)
            {
                return BadRequest();
            }

            Mapper.Map(model, user.ModulePrivilege);
            await _userServices.EditAsync(user);

            Messages.AddSuccess("Privileges edite");

            return RedirectToAction(nameof(ModulePrivileges), new { id = user.Id });
        }
    }
}
