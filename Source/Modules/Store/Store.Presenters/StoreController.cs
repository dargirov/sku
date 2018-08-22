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
        private readonly IMemoServices _memoServices;
        private readonly IGridServices _gridServices;

        public StoreController(IStoreServices storeServices, ICityServices cityServices, IUserServices userServices, IMemoServices memoServices, IGridServices gridServices)
        {
            _storeServices = storeServices;
            _cityServices = cityServices;
            _userServices = userServices;
            _memoServices = memoServices;
            _gridServices = gridServices;
        }

        public async Task<IActionResult> Index(IndexRequesModel model)
        {
            var pageSize = await _gridServices.UpdateAndGetPageSizeAsync("StoreIndex", model.PageSize, Messages);

            var storesAndPageData = await _storeServices.GetListAsync(
                model.Page,
                pageSize,
                model.SortColumn,
                model.SortDirection,
                model.SearchCriteria?.Name,
                model.SearchCriteria?.CityId,
                model.SearchCriteria?.Address);

            var viewModel = new IndexViewModel()
            {
                Stores = storesAndPageData,
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
                if (await _storeServices.EditAsync(store, Messages))
                {
                    Messages.AddSuccess("Store Edited");
                }
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
                Stores = await _storeServices.GetListWithoutPrivCheckAsync(),
                UserId = id,
                StorePrivileges = await _storeServices.GetPrivilegeForUserListAsync(id)
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Privileges(PrivilegeRequestModel model)
        {
            var user = await _userServices.GetByIdAsync(model.UserId);
            if (user == null)
            {
                return BadRequest();
            }

            await _storeServices.EditPrivilegesAsync(user.Id, model.StorePrivileges, Messages);

            return RedirectToAction(nameof(Privileges), new { id = model.UserId });
        }

        [HttpGet]
        public async Task<IActionResult> History(int id, int page)
        {
            var store = await _storeServices.GetByIdAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            var memos = await _memoServices.GetMemosAsync(store.Id, store.GetType().Name, page, 10);
            var viewModel = new Administration.Presenters.Dtos.HistoryViewModel
            {
                Id = store.Id,
                Memos = memos
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Config(int id)
        {
            var store = await _storeServices.GetByIdAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            var viewModel = Mapper.Map<ConfigViewModel>(store);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Config(ConfigRequestModel model)
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
                store.Color = !string.IsNullOrWhiteSpace(model.Color) ? model.Color : null;
                if (await _storeServices.EditAsync(store, Messages))
                {
                    Messages.AddSuccess("Store Edited");
                }
            }

            return RedirectToAction(nameof(Config), new { id = store?.Id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var store = await _storeServices.GetByIdAsync(id);
            if (store == null)
            {
                return BadRequest();
            }

            await _storeServices.DeleteAsync(store, Messages);

            return RedirectToAction(nameof(Index));
        }
    }
}
