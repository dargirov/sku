using Administration.Presenters;
using Infrastructure.Services.Common;
using Manufacturer.Bll;
using Microsoft.AspNetCore.Mvc;
using Product.Bll;
using Product.Entities;
using Request.Bll;
using Request.Presenters.Dtos;
using Store.Bll;
using Supplier.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Request.Presenters
{
    public class RequestController : BaseController
    {
        private readonly IProductServices _productServices;
        private readonly IStoreServices _storeServices;
        private readonly ISupplierServices _supplierServices;
        private readonly IManufacturerServices _manufacturerServices;
        private readonly IRequestServices _requestServices;

        public RequestController(IProductServices productServices, IStoreServices storeServices, ISupplierServices supplierServices, IManufacturerServices manufacturerServices, IRequestServices requestServices)
        {
            _productServices = productServices;
            _storeServices = storeServices;
            _supplierServices = supplierServices;
            _manufacturerServices = manufacturerServices;
            _requestServices = requestServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index(IndexRequestModel model)
        {
            var (products, pageData) = await _productServices.GetListAsync(
                model.Page,
                model.PageSize, // its not set
                model.SortColumn,
                model.SortDirection,
                model.SearchCriteria?.Name,
                model.SearchCriteria?.StoreId,
                model.SearchCriteria?.CategoryId,
                model.SearchCriteria?.ManufacturerId,
                model.SearchCriteria?.SupplierId,
                model.SearchCriteria?.Description);

            var stores = await _storeServices.GetListAsync();
            var categories = await _productServices.GetCategoryListAsync();
            var suppliers = await _supplierServices.GetListAsync();
            var manufacturers = await _manufacturerServices.GetListAsync();

            List<(int storeId, int quantity)> storeQuantities = products
                .SelectMany(p => p.Variants.SelectMany(v => v.Stocks))
                .Select(x => (storeId: x.StoreId, quantity: x.Quantity))
                .ToList();

            var newRequests = await _requestServices.GetListAsync(1, 10, Entities.RequestStatusEnum.New);

            var viewModel = new IndexViewModel()
            {
                Products = (products, pageData),
                StoreId = stores.FirstOrDefault()?.Id ?? 0,
                StoreQuantities = storeQuantities,
                Stores = stores.ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, false),
                NewRequests = newRequests.requests,
                SearchCriteria = new IndexSearchCriteria()
                {
                    Stores = stores.ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, true),
                    Categories = categories.ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, true),
                    Manufacturers = manufacturers.ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, true),
                    Suppliers = suppliers.ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, true)
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody]IndexCreateRequestModel model)
        {
            if (!IsAjax())
            {
                return RedirectToAction(nameof(Index));
            }

            Entities.Request request = model.CreateNewRequest
                ? new Entities.Request() { Status = Entities.RequestStatusEnum.New, StockRequests = new List<Entities.StockRequest>() }
                : await _requestServices.GetByIdAsync(model.RequestId);

            if (request == null)
            {
                return BadRequest();
            }

            //foreach (var stockRequestDto in model.StockRequests)
            //{
            //    var fromStore = await _storeServices.GetByIdAsync(stockRequestDto.FromStoreId);
            //    var toStore = await _storeServices.GetByIdAsync(stockRequestDto.FromStoreId);

            //    var stockRequest = request.StockRequests.FirstOrDefault(x => x.StockId == stockRequestDto.StockId);
            //    if (stockRequest == null)
            //    {
            //        request.StockRequests.Add(new Entities.StockRequest()
            //        {
            //            StockId = stockRequestDto.StockId,
            //            FromStore = fromStore,
            //            ToStore = toStore,
            //            Quantity = stockRequestDto.Quantity
            //        });
            //    }
            //    else
            //    {
            //        stockRequest.FromStore = fromStore;
            //        stockRequest.ToStore = toStore;
            //        stockRequest.Quantity = stockRequestDto.Quantity;
            //    }
            //}

            await _requestServices.EditAsync(request, model.StockRequests);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> List(ListRequestModel model)
        {
            var requests = await _requestServices.GetListAsync(model.Page, model.PageSize, model.SortColumn, model.SortDirection);

            var viewModel = new ListViewModel()
            {
                Requests = requests
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var stores = await _storeServices.GetListAsync();
            var request = await _requestServices.GetByIdAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            var viewModel = Mapper.Map<EditViewModel>(request);
            viewModel.ToStores = stores;
            viewModel.Priorities = Enum.GetValues(typeof(ProductPriorityEnum)).Cast<ProductPriorityEnum>().ToSelectList(x => ((int)x).ToString(), x => x.ToString(), ProductPriorityEnum.Normal.ToString(), false);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRequestModel model)
        {
            var request = await _requestServices.GetByIdAsync(model.Id);
            if (request == null)
            {
                return NotFound();
            }

            request.Text = model.Text;
            await _requestServices.EditAsync(request, model.StockRequests);

            return RedirectToAction(nameof(Edit).ToLower(), new { id = model.Id });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRequestStock(int requestStockId)
        {
            var stockRequest = await _requestServices.GetStockRequestByIdAsync(requestStockId);
            if (stockRequest == null)
            {
                return NotFound();
            }

            var result = await _requestServices.DeleteStockRequestAsync(stockRequest);
            if (result == default(int))
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> EditNext(int id)
        {
            var request = await _requestServices.GetByIdAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            var newStatus = _requestServices.GetNextStatus(request.Status);
            if (!newStatus.HasValue)
            {
                Messages.AddError($"Can not find next status for status {request.Status}");
            }
            else
            {
                request.Status = newStatus.Value;
                await _requestServices.EditAsync(request);
            }

            return RedirectToAction(nameof(Edit).ToLower(), new { id = id });
        }
    }
}
