using Administration.Bll;
using Administration.Presenters;
using Client.Bll;
using Client.Entities;
using Client.Presenters.Dtos;
using Infrastructure.Services.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Client.Presenters
{
    [Authorize(Policy = "LoggedIn")]
    public class ClientController : BaseController
    {
        private readonly IClientServices _clientServices;
        private readonly ICityServices _cityServices;
        private readonly ICountryServices _countryServices;

        public ClientController(IClientServices clientServices, ICityServices cityServices, ICountryServices countryServices)
        {
            _clientServices = clientServices;
            _cityServices = cityServices;
            _countryServices = countryServices;
        }

        public async Task<IActionResult> Index(IndexRequestModel model)
        {
            var clients = await _clientServices.GetListAsync(
                model.SearchCriteria?.MolName,
                model.SearchCriteria?.FirmaNamePersonlNo,
                model.SearchCriteria?.CityId,
                model.SearchCriteria?.Address,
                model.SearchCriteria?.Phone,
                model.SearchCriteria?.Email);

            var cities = await _cityServices.GetListAsync();

            if (IsAjax())
            {
                return Json(new { data = clients });
            }

            var viewModel = new IndexViewModel()
            {
                Clients = clients,
                SearchCriteria = new IndexSearchCriteria()
                {
                    Cities = cities.ToSelectList(c => c.Id.ToString(), c => c.Name, string.Empty, true),
                }
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditLegal(int id)
        {
            var cities = await _cityServices.GetListAsync();
            var countries = await _countryServices.GetListAsync();

            if (id == 0)
            {
                return View(new EditLegalViewModel()
                {
                    Cities = cities.ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, true),
                    Countries = countries.ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, true)
                });
            }

            var client = await _clientServices.GetByIdAsync(id, ClientTypeEnum.Legal);

            if (client == null)
            {
                return BadRequest();
            }

            var viewModel = Mapper.Map<EditLegalViewModel>(client as LegalClient);
            viewModel.Cities = cities.ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, false);
            viewModel.Countries = countries.ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, false);

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditNatural(int id)
        {
            var cities = await _cityServices.GetListAsync();
            var countries = await _countryServices.GetListAsync();

            if (id == 0)
            {
                return this.View(new EditNaturalViewModel()
                {
                    Cities = cities.ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, true),
                    Countries = countries.ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, true)
                });
            }

            var client = await _clientServices.GetByIdAsync(id, ClientTypeEnum.Natural);

            if (client == null)
            {
                return BadRequest();
            }

            var viewModel = Mapper.Map<EditNaturalViewModel>(client as NaturalClient);
            viewModel.Cities = cities.ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, false);
            viewModel.Countries = countries.ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, false);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLegal(EditLegalRequestModel model)
        {
            var client = await _clientServices.GetByIdAsync(model.Id, ClientTypeEnum.Legal);

            if (model.Id > 0 && client == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ModelState.GetErrors().ForEach(x => Messages.AddWarning(x));
            }
            else
            {
                client = Mapper.Map(model, client as LegalClient);
                await _clientServices.EditAsync(client);
                Messages.AddSuccess("Legal client Edited");
            }

            return RedirectToAction(nameof(EditLegal), new { id = client?.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNatural(EditNaturalRequestModel model)
        {
            var client = await _clientServices.GetByIdAsync(model.Id, ClientTypeEnum.Natural);

            if (model.Id > 0 && client == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ModelState.GetErrors().ForEach(x => Messages.AddWarning(x));
            }
            else
            {
                client = Mapper.Map(model, client as NaturalClient);
                await _clientServices.EditAsync(client);
                Messages.AddSuccess("Natural client Edited");
            }

            return this.RedirectToAction(nameof(EditNatural), new { id = client?.Id });
        }
    }
}
