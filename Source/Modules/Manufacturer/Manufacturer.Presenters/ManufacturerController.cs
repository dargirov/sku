using Administration.Bll;
using Administration.Presenters;
using Infrastructure.Services.Common;
using Manufacturer.Bll;
using Manufacturer.Presenters.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Manufacturer.Presenters
{
    public class ManufacturerController : BaseController
    {
        private readonly IManufacturerServices _manufacturerServices;
        private readonly ICountryServices _countryServices;

        public ManufacturerController(IManufacturerServices manufacturerServices, ICountryServices countryServices)
        {
            _manufacturerServices = manufacturerServices;
            _countryServices = countryServices;
        }

        public async Task<IActionResult> Index(IndexRequestModel model)
        {
            var manufactirers = await _manufacturerServices.GetListAsync(
                model.SearchCriteria?.Name,
                model.SearchCriteria?.CountryId,
                model.SearchCriteria?.Email);

            var viewModel = new IndexViewModel()
            {
                Manufacturers = manufactirers,
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
                await _manufacturerServices.EditAsync(manufacturer);
                Messages.AddSuccess("Manufacturer Edited");
            }

            return RedirectToAction(nameof(Edit), new { id = manufacturer?.Id });
        }
    }
}
