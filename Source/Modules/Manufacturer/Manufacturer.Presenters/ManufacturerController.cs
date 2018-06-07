using Administration.Bll;
using Administration.Presenters;
using Infrastructure.Services.Common;
using Manufacturer.Bll;
using Manufacturer.Presenters.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Manufacturer.Presenters
{
    [Authorize(Policy = "LoggedIn")]
    public class ManufacturerController : BaseController
    {
        private readonly IManufacturerServices _manufacturerServices;
        private readonly ICountryServices _countryServices;
        private readonly IMemoServices _memoServices;
        private readonly IGridServices _gridServices;

        public ManufacturerController(IManufacturerServices manufacturerServices, ICountryServices countryServices, IMemoServices memoServices, IGridServices gridServices)
        {
            _manufacturerServices = manufacturerServices;
            _countryServices = countryServices;
            _memoServices = memoServices;
            _gridServices = gridServices;
        }

        public async Task<IActionResult> Index(IndexRequestModel model)
        {
            var pageSize = await _gridServices.UpdateAndGetPageSizeAsync("ManufacturerIndex", model.PageSize, Messages);

            var manufactirersAndPageData = await _manufacturerServices.GetListAsync(
                model.Page,
                pageSize,
                model.SortColumn,
                model.SortDirection,
                model.SearchCriteria?.Name,
                model.SearchCriteria?.CountryId,
                model.SearchCriteria?.Email);

            var viewModel = new IndexViewModel()
            {
                Manufacturers = manufactirersAndPageData,
                SearchCriteria = model.SearchCriteria,
                Countries = (await _countryServices.GetListAsync()).ToSelectList(c => c.Id.ToString(), c => c.Name, string.Empty, true)
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var countries = await _countryServices.GetListAsync();

            if (id == 0)
            {
                return View(new EditViewModel()
                {
                    Countries = countries.ToSelectList(c => c.Id.ToString(), c => c.Name, string.Empty, true)
                });
            }

            var manufacturer = await _manufacturerServices.GetByIdAsync(id);
            if (manufacturer == null)
            {
                return BadRequest();
            }

            var viewModel = Mapper.Map<EditViewModel>(manufacturer);
            viewModel.Countries = countries.ToSelectList(c => c.Id.ToString(), c => c.Name, string.Empty, true);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRequestModel model)
        {
            var manufacturer = await _manufacturerServices.GetByIdAsync(model.Id);
            if (model.Id > 0 && manufacturer == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ModelState.GetErrors().ForEach(x => Messages.AddWarning(x));
            }
            else
            {
                manufacturer = Mapper.Map(model, manufacturer);
                if (await _manufacturerServices.EditAsync(manufacturer, Messages))
                {
                    Messages.AddSuccess("Manufacturer Edited");
                }
            }

            return RedirectToAction(nameof(Edit), new { id = manufacturer?.Id });
        }

        [HttpGet]
        public async Task<IActionResult> History(int id, int page)
        {
            var manufacturer = await _manufacturerServices.GetByIdAsync(id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            var memos = await _memoServices.GetMemosAsync(manufacturer.Id, manufacturer.GetType().Name, page, 10);
            var viewModel = new Administration.Presenters.Dtos.HistoryViewModel
            {
                Id = manufacturer.Id,
                Memos = memos
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var manufacturer = await _manufacturerServices.GetByIdAsync(id);
            if (manufacturer == null)
            {
                return BadRequest();
            }

            await _manufacturerServices.DeleteAsync(manufacturer, Messages);

            return RedirectToAction(nameof(Index));
        }
    }
}
