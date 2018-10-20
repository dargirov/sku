using Administration.Bll;
using Administration.Presenters;
using Infrastructure.Services.Common;
using Manufacturer.Bll;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = "LoggedIn")]
    public class RequestController : BaseController
    {
        private readonly IProductServices _productServices;
        private readonly IStoreServices _storeServices;
        private readonly ISupplierServices _supplierServices;
        private readonly IManufacturerServices _manufacturerServices;
        private readonly IRequestServices _requestServices;
        private readonly IPriorityServices _priorityServices;
        private readonly IAuthenticationServices _authenticationServices;
        private readonly IMemoServices _memoServices;
        private readonly IGridServices _gridServices;

        public RequestController(IProductServices productServices, IStoreServices storeServices, ISupplierServices supplierServices, IManufacturerServices manufacturerServices, IRequestServices requestServices, IPriorityServices priorityServices, IAuthenticationServices authenticationServices, IMemoServices memoServices, IGridServices gridServices)
        {
            _productServices = productServices;
            _storeServices = storeServices;
            _supplierServices = supplierServices;
            _manufacturerServices = manufacturerServices;
            _requestServices = requestServices;
            _priorityServices = priorityServices;
            _authenticationServices = authenticationServices;
            _memoServices = memoServices;
            _gridServices = gridServices;
        }

        public async Task<IActionResult> Index(IndexRequestModel model)
        {
            var pageSize = await _gridServices.UpdateAndGetPageSizeAsync("RequestIndex", model.PageSize, Messages);

            var (products, pageData) = await _productServices.GetListAsync(
                model.Page,
                pageSize,
                model.SortColumn,
                model.SortDirection,
                model.SearchCriteria?.Name,
                model.SearchCriteria?.StoreId,
                model.SearchCriteria?.CategoryId,
                model.SearchCriteria?.ManufacturerId,
                model.SearchCriteria?.SupplierId,
                model.SearchCriteria?.Description);

            var user = await _authenticationServices.GetCurrentUserAsync();
            var stores = await _storeServices.GetListAsync();
            var categories = await _productServices.GetCategoryListAsync();
            var suppliers = await _supplierServices.GetListAsync();
            var manufacturers = await _manufacturerServices.GetListAsync();

            List<(int storeId, int quantity)> storeQuantities = products
                .SelectMany(p => p.Variants.SelectMany(v => v.Stocks))
                .Select(x => (storeId: x.StoreId, quantity: x.Quantity))
                .ToList();

            var productsPriority = new Dictionary<int, ProductPriorityEnum>();

            foreach (var productId in products.Select(x => x.Id).Distinct())
            {
                var priority = (await _priorityServices.GetListAsync(productId, x => x.User == user)).FirstOrDefault();
                productsPriority.Add(productId, priority?.Priority ?? ProductPriorityEnum.Normal);
            }

            var newRequests = await _requestServices.GetListAsync(1, 10, Entities.RequestStatusEnum.New);

            var viewModel = new IndexViewModel()
            {
                Products = (products, pageData),
                StoreId = stores.FirstOrDefault()?.Id ?? 0,
                StoreQuantities = storeQuantities,
                Stores = stores.ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, false),
                NewRequests = newRequests.requests,
                ProductsPriority = productsPriority,
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
        public async Task<IActionResult> IndexRequest([FromBody]IndexCreateRequestModel model)
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

            await _requestServices.EditAsync(request, model.StockRequests, Messages);

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditRequestModel model)
        {
            var request = await _requestServices.GetByIdAsync(model.Id);
            if (request == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ModelState.GetErrors().ForEach(x => Messages.AddWarning(x));
                return RedirectToAction(nameof(Edit).ToLower(), new { id = model.Id });
            }

            if (!model.IsNextStep)
            {
                request.Text = model.Text;
                if (await _requestServices.EditAsync(request, model.StockRequests, Messages))
                {
                    Messages.AddSuccess("Request Edited");
                }

                return RedirectToAction(nameof(Edit).ToLower(), new { id = model.Id });
            }

            if (request.Status == Entities.RequestStatusEnum.New)
            {
                request.Text = model.Text;
                request.Status = Entities.RequestStatusEnum.Sent;
                if (await _requestServices.EditAsync(request, model.StockRequests, Messages))
                {
                    Messages.AddSuccess("Request Edited");
                }
            }
            else if (request.Status == Entities.RequestStatusEnum.Sent)
            {
                if (!await _requestServices.EditProductsQuantityAsync(request, model.StockRequests, Messages))
                {
                    Messages.AddSuccess("Error changing product quantities");
                    return RedirectToAction(nameof(Edit).ToLower(), new { id = model.Id });
                }

                request.Status = request.StockRequests.Any(x => x.Quantity != 0) 
                    ? Entities.RequestStatusEnum.PartiallyCompleted 
                    : Entities.RequestStatusEnum.Completed;

                if (!await _requestServices.EditAsync(request, Messages))
                {
                    Messages.AddSuccess("Error saving request status");
                    return RedirectToAction(nameof(Edit).ToLower(), new { id = model.Id });
                }
            }

            return RedirectToAction(nameof(Edit).ToLower(), new { id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> History(int id)
        {
            var request = await _requestServices.GetByIdAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            var memos = await _memoServices.GetMemosAsync(request.Id, request.GetType().Name, 1, 10);
            var viewModel = new Administration.Presenters.Dtos.HistoryViewModel()
            {
                Id = request.Id,
                Memos = memos
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRequestStock(int requestStockId)
        {
            var stockRequest = await _requestServices.GetStockRequestByIdAsync(requestStockId);
            if (stockRequest == null)
            {
                return NotFound();
            }

            if (!await _requestServices.DeleteStockRequestAsync(stockRequest, Messages))
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
