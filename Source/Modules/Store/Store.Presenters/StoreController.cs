using Administration.Bll;
using Administration.Presenters;
using Infrastructure.Services.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Bll;
using Store.Presenters.Dtos;
using System.Threading.Tasks;

namespace Store.Presenters
{
    [Authorize(Policy = "LoggedIn")]
    public class StoreController : BaseController
    {
        private readonly IStoreServices _storeServices;
        private readonly ICityServices _cityServices;
        private readonly IUserServices _userServices;

        public StoreController(IStoreServices storeServices, ICityServices cityServices, IUserServices userServices)
        {
            _storeServices = storeServices;
            _cityServices = cityServices;
            _userServices = userServices;
        }

        public async Task<IActionResult> Index(IndexRequesModel model)
        {
            var stores = await _storeServices.GetListAsync(
                model.SearchCriteria?.Name,
                model.SearchCriteria?.CityId,
                model.SearchCriteria?.Address);

            var viewModel = new IndexViewModel()
            {
                Stores = stores,
                SearchCriteria = new IndexSearchCriteria()
                {
                    Cities = (await _storeServices.GetCityListAsync()).ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, true)
                }
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cities = await _cityServices.GetListAsync();
            if (id == 0)
            {
                return View(new EditViewModel() { Cities = cities.ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, false) });
            }

            var store = await _storeServices.GetByIdAsync(id);
            if (store == null)
            {
                return BadRequest();
            }

            var viewModel = Mapper.Map<EditViewModel>(store);
            viewModel.Cities = cities.ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, false);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditRequestModel model)
        {
            var store = await _storeServices.GetByIdAsync(model.Id);
            if (model.Id > 0 && store == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ModelState.GetErrors().ForEach(x => Messages.AddWarning(x));
            }
            else
            {
                store = Mapper.Map(model, store);
                await _storeServices.EditAsync(store);
                Messages.AddSuccess("Store Edited");
            }

            return RedirectToAction(nameof(Edit), new { id = store?.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Privileges(int id)
        {
            var user = await _userServices.GetByIdAsync(id);
            if (user.IsAdmin)
            {
                return BadRequest();
            }

            var viewModel = new PrivilegesViewModel()
            {
                Stores = await _storeServices.GetListAsync(),
                UserId = id,
                StorePrivileges = await _storeServices.GetPrivilegeForUserListAsync(id)
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Privileges(PrivilegeRequestModel model)
        {
            var user = await _userServices.GetByIdAsync(model.UserId);
            if (user == null)
            {
                return BadRequest();
            }

            await _storeServices.EditPrivilegesAsync(user.Id, model.StorePrivileges);

            //if (user.StorePrivileges.Count == 0)
            //{
            //    user.StorePrivileges = model.StorePrivileges.ToList();
            //}
            //else
            //{
            //    foreach (var priv in model.StorePrivileges)
            //    {
            //        var userPriv = user.StorePrivileges.FirstOrDefault(x => x.Id == priv.Id);
            //        if (userPriv != null)
            //        {
            //            userPriv.Read = priv.Read;
            //            userPriv.Write = priv.Write;
            //            userPriv.Delete = priv.Delete;
            //        }
            //        else
            //        {
            //            user.StorePrivileges.Add(priv);
            //        }
            //    }
            //}

            //await this.usersService.SaveAsync(user);

            return RedirectToAction(nameof(Privileges), new { id = model.UserId });
        }
    }
}
