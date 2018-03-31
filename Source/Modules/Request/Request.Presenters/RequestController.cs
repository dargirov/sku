using Administration.Presenters;
using Infrastructure.Services.Common;
using Manufacturer.Bll;
using Microsoft.AspNetCore.Mvc;
using Product.Bll;
using Request.Presenters.Dtos;
using Store.Bll;
using Supplier.Bll;
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

        public RequestController(IProductServices productServices, IStoreServices storeServices, ISupplierServices supplierServices, IManufacturerServices manufacturerServices)
        {
            _productServices = productServices;
            _storeServices = storeServices;
            _supplierServices = supplierServices;
            _manufacturerServices = manufacturerServices;
        }

        public async Task<IActionResult> Index([FromQuery]IndexRequestModel model)
        {
            var (products, pageData) = await _productServices.GetListAsync(
                model.Page, // its not set
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

            var viewModel = new IndexViewModel()
            {
                Products = (products, pageData),
                StoreId = stores.FirstOrDefault()?.Id ?? 0,
                StoreQuantities = storeQuantities,
                Stores = stores.ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, false),
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //if (id == 0)
            //{
            //    return View(new EditViewModel()
            //    {
            //        Stores = stores,
            //        Categories = categories.ToSelectList(c => c.Id.ToString(), c => c.Name, string.Empty, false),
            //        Manufacturers = manufacturers.ToSelectList(c => c.Id.ToString(), c => c.Name, string.Empty, false),
            //        Suppliers = suppliers.ToSelectList(s => s.Id.ToString(), s => s.Name, string.Empty, true)
            //    });
            //}
            return null;
        }
    }
}
