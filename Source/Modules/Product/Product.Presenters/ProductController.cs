using Administration.Presenters;
using Infrastructure.Services.Common;
using Infrastructure.Services.ContentServer;
using Manufacturer.Bll;
using Microsoft.AspNetCore.Mvc;
using Product.Bll;
using Product.Presenters.Dtos;
using Store.Bll;
using Supplier.Bll;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Presenters
{
    public class ProductController : BaseController
    {
        private readonly IProductServices _productServices;
        private readonly ISupplierServices _supplierServices;
        private readonly IContentServer _contentServer;
        private readonly IManufacturerServices _manufacturerServices;
        private readonly IStoreServices _storeServices;

        public ProductController(IProductServices productServices, ISupplierServices supplierServices, IContentServer contentServer, IManufacturerServices manufacturerServices, IStoreServices storeServices)
        {
            _productServices = productServices;
            _supplierServices = supplierServices;
            _contentServer = contentServer;
            _manufacturerServices = manufacturerServices;
            _storeServices = storeServices;
        }

        public async Task<IActionResult> Index([FromQuery]IndexRequestModel model)
        {
            if (IsAjax())
            {
                var (products, count) = await _productServices.GetListAsync(
                    model.Start,
                    model.Length,
                    model.Order[0]["column"],
                    model.Order[0]["dir"],
                    model.SearchCriteria?.Name,
                    model.SearchCriteria?.CategoryId,
                    model.SearchCriteria?.SupplierId,
                    model.SearchCriteria?.Warranty,
                    model.SearchCriteria?.Description);

                var result = new List<Dictionary<string, string>>();
                foreach (var product in products)
                {
                    result.Add(new Dictionary<string, string>()
                    {
                        { "id", product.Id.ToString() },
                        { "picture", product.Pictures.Any() ? Url.Action("index", "contentserver", new { id = product.Pictures.FirstOrDefault()?.Thumb.Guid, area = string.Empty }) : null },
                        { "productName", product.Name },
                        { "categoryName", product.Category.Name },
                        { "manufacturerName", product.Manufacturer.Name },
                        { "createdOn", product.CreatedOn.ToString("dd.MM.yyyy") },
                        { "variants", string.Join(", ", product.Variants.Select(x => $"{x.Code} - {x.PriceCustomer}")) }
                    });
                }

                return Json(new { data = result, recordsTotal = count, recordsFiltered = count });
            }

            var categories = await _productServices.GetCategoryListAsync();
            var suppliers = await _supplierServices.GetListAsync();

            var viewModel = new IndexViewModel()
            {
                Products = new List<Entities.Product>(),
                SearchCriteria = new IndexSearchCriteria()
                {
                    Categories = categories.ToSelectList(c => c.Id.ToString(), c => c.Name, string.Empty, true),
                    Suppliers = suppliers.ToSelectList(s => s.Id.ToString(), s => s.Name, string.Empty, true)
                }
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var stores = await _storeServices.GetListAsync();
            var categories = await _productServices.GetCategoryListAsync();
            var manufacturers = await _manufacturerServices.GetListAsync();
            var suppliers = await _supplierServices.GetListAsync();
            //var user = await usersService.GetByIdAsync(cacheService.Get<int>("user_id"));

            if (id == 0)
            {
                return View(new EditViewModel()
                {
                    Stores = stores,
                    Categories = categories.ToSelectList(c => c.Id.ToString(), c => c.Name, string.Empty, false),
                    Manufacturers = manufacturers.ToSelectList(c => c.Id.ToString(), c => c.Name, string.Empty, false),
                    Suppliers = suppliers.ToSelectList(s => s.Id.ToString(), s => s.Name, string.Empty, true)
                });
            }

            var product = await _productServices.GetByIdAsync(id);

            if (product == null)
            {
                return BadRequest();
            }

            var viewModel = Mapper.Map<EditViewModel>(product);

            viewModel.Stores = stores;
            //viewModel.CategoryId = product.CategoryId.ToString();
            viewModel.Categories = categories.ToSelectList(c => c.Id.ToString(), c => c.Name, viewModel.CategoryId, false);
            //viewModel.ManufacturerId = product.ManufacturerId.ToString();
            viewModel.Manufacturers = manufacturers.ToSelectList(s => s.Id.ToString(), s => s.Name, viewModel.SupplierId, false);
            //viewModel.SupplierId = product.SupplierId.ToString();
            viewModel.Suppliers = suppliers.ToSelectList(s => s.Id.ToString(), s => s.Name, viewModel.SupplierId, true);
            //viewModel.StorePrivileges = user.StorePrivileges;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditRequestModel model)
        {
            var product = await _productServices.GetByIdAsync(model.Id);
            if (model.Id > 0 && product == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ModelState.GetErrors().ForEach(x => Messages.AddWarning(x));
            }
            else
            {
                var pictures = product?.Pictures;
                product = Mapper.Map<Entities.Product>(model);
                //var pictures = product.Pictures ?? new List<ProductPicture>();
                product.Pictures = pictures ?? new List<Entities.ProductPicture>();

                if (model.FormFiles != null)
                {
                    foreach (var picture in model.FormFiles)
                    {
                        if (picture.Length > 0)
                        {
                            var fullSize = await _contentServer.SaveAsync(picture);
                            var thumb = await _contentServer.ResizeAndSaveAsync(picture);
                            product.Pictures.Add(new Entities.ProductPicture()
                            {
                                FullSize = fullSize,
                                Thumb = thumb
                            });
                        }
                    }
                }

                //product.Pictures = pictures;
                await _productServices.EditAsync(product);
                Messages.AddSuccess("Product Edited");
            }

            return RedirectToAction(nameof(Edit), new { id = product?.Id });
        }

        public async Task<IActionResult> Categories()
        {
            var categories = await _productServices.GetCategoryListAsync();
            var viewModel = new CategoriesViewModel() { Categories = categories };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            if (id == 0)
            {
                return View(new EditCategoryViewModel());
            }

            var category = await _productServices.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return BadRequest();
            }

            var viewModel = Mapper.Map<EditCategoryViewModel>(category);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(EditCategoryRequestModel model)
        {
            var category = await _productServices.GetCategoryByIdAsync(model.Id);
            if (model.Id > 0 && category == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ModelState.GetErrors().ForEach(x => Messages.AddWarning(x));
            }
            else
            {
                category = Mapper.Map(model, category);
                await _productServices.EditCategoryAsync(category);
                Messages.AddSuccess("Store Edited");
            }

            return RedirectToAction(nameof(EditCategory), new { id = category?.Id });
        }
    }
}
