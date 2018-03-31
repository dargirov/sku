﻿using Administration.Presenters;
using Infrastructure.Services.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Supplier.Bll;
using Supplier.Presenters.Dtos;
using System.Threading.Tasks;

namespace Supplier.Presenters
{
    [Authorize(Policy = "LoggedIn")]
    public class SupplierController : BaseController
    {
        private readonly ISupplierServices _supplierServices;

        public SupplierController(ISupplierServices supplierServices)
        {
            _supplierServices = supplierServices;
        }

        public async Task<IActionResult> Index(IndexRequestModel model)
        {
            var suppliersAndPageData = await _supplierServices.GetListAsync(
                model.Page,
                model.PageSize,
                model.SortColumn,
                model.SortDirection,
                model.SearchCriteria?.Name,
                model.SearchCriteria?.Mol,
                model.SearchCriteria?.Phone,
                model.SearchCriteria?.Address,
                model.SearchCriteria?.Email,
                model.SearchCriteria?.Url);

            var viewModel = new IndexViewModel()
            {
                Suppliers = suppliersAndPageData,
                SearchCriteria = new IndexSearchCriteria()
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return View(new EditViewModel());
            }

            var supplier = await _supplierServices.GetByIdAsync(id);
            if (supplier == null)
            {
                return BadRequest();
            }

            var viewModel = Mapper.Map<EditViewModel>(supplier);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditRequestModel model)
        {
            var supplier = await _supplierServices.GetByIdAsync(model.Id);
            if (model.Id > 0 && supplier == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ModelState.GetErrors().ForEach(x => Messages.AddWarning(x));
            }
            else
            {
                supplier = Mapper.Map(model, supplier);
                await _supplierServices.EditAsync(supplier);
                Messages.AddSuccess("Supplier Edited");
            }

            return RedirectToAction(nameof(Edit), new { id = supplier?.Id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _supplierServices.GetByIdAsync(id);
            if (supplier == null)
            {
                return BadRequest();
            }

            await _supplierServices.DeleteAsync(supplier, Messages);

            return RedirectToAction(nameof(Index));
        }
    }
}
