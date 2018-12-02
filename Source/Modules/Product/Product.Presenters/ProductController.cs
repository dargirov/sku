using Administration.Bll;
using Administration.Presenters;
using Infrastructure.Data.Common;
using Infrastructure.Services.Common;
using Infrastructure.Services.ContentServer;
using Manufacturer.Bll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Product.Bll;
using Product.Bll.Dtos.Import;
using Product.Entities;
using Product.Presenters.Dtos;
using Store.Bll;
using Supplier.Bll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Presenters
{
    [Authorize(Policy = "LoggedIn")]
    public class ProductController : BaseController
    {
        private readonly IProductServices _productServices;
        private readonly ISupplierServices _supplierServices;
        private readonly IContentServer _contentServer;
        private readonly IManufacturerServices _manufacturerServices;
        private readonly IStoreServices _storeServices;
        private readonly IPriorityServices _priorityServices;
        private readonly IAuthenticationServices _authenticationServices;
        private readonly IMemoServices _memoServices;
        private readonly IGridServices _gridServices;

        public ProductController(IProductServices productServices, ISupplierServices supplierServices, IContentServer contentServer, IManufacturerServices manufacturerServices, IStoreServices storeServices, IPriorityServices priorityServices, IAuthenticationServices authenticationServices, IMemoServices memoServices, IGridServices gridServices)
        {
            _productServices = productServices;
            _supplierServices = supplierServices;
            _contentServer = contentServer;
            _manufacturerServices = manufacturerServices;
            _storeServices = storeServices;
            _priorityServices = priorityServices;
            _authenticationServices = authenticationServices;
            _memoServices = memoServices;
            _gridServices = gridServices;
        }

        public async Task<IActionResult> Index(IndexRequestModel model)
        {
            var pageSize = await _gridServices.UpdateAndGetPageSizeAsync("ProductIndex", model.PageSize, Messages);

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

            var stores = await _storeServices.GetListAsync();
            var categories = await _productServices.GetCategoryListAsync();
            var suppliers = await _supplierServices.GetListAsync();
            var manufacturers = await _manufacturerServices.GetListAsync();

            var viewModel = new IndexViewModel()
            {
                Products = (products, pageData),
                SearchCriteria = new IndexSearchCriteria()
                {
                    Stores = stores.ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, true),
                    Categories = categories.OrderBy(x => x.Name).ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, true),
                    Manufacturers = manufacturers.OrderBy(x => x.Name).ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, true),
                    Suppliers = suppliers.OrderBy(x => x.Name).ToSelectList(x => x.Id.ToString(), x => x.Name, string.Empty, true)
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

            if (id == 0)
            {
                return View(new EditViewModel()
                {
                    Stores = stores,
                    Categories = categories.OrderBy(x => x.Name).ToSelectList(c => c.Id.ToString(), c => c.Name, string.Empty, false),
                    Manufacturers = manufacturers.OrderBy(x => x.Name).ToSelectList(c => c.Id.ToString(), c => c.Name, string.Empty, false),
                    Suppliers = suppliers.OrderBy(x => x.Name).ToSelectList(s => s.Id.ToString(), s => s.Name, string.Empty, true)
                });
            }

            var product = await _productServices.GetByIdAsync(id);

            if (product == null)
            {
                return BadRequest();
            }

            var viewModel = Mapper.Map<EditViewModel>(product);
            var settings = await _productServices.GetSettingsAsync();

            viewModel.Stores = stores;
            viewModel.Categories = categories.OrderBy(x => x.Name).ToSelectList(c => c.Id.ToString(), c => c.Name, viewModel.CategoryId, false);
            viewModel.Manufacturers = manufacturers.OrderBy(x => x.Name).ToSelectList(s => s.Id.ToString(), s => s.Name, viewModel.SupplierId, false);
            viewModel.Suppliers = suppliers.OrderBy(x => x.Name).ToSelectList(s => s.Id.ToString(), s => s.Name, viewModel.SupplierId, true);
            viewModel.HideMainInfo = settings?.HideMainInfo ?? false;

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
                product = Mapper.Map(model, product);
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

                await _productServices.EditAsync(product, model.Variants, Messages);
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
                if (await _productServices.EditCategoryAsync(category, Messages))
                {
                    Messages.AddSuccess("Store Edited");
                }
            }

            return RedirectToAction(nameof(EditCategory), new { id = category?.Id });
        }

        public async Task<IActionResult> Pictures(int id)
        {
            var product = await _productServices.GetByIdAsync(id);
            if (product == null)
            {
                return BadRequest();
            }

            var viewModel = new PicturesViewModel()
            {
                Id = product.Id,
                Pictures = product.Pictures
            };

            return View(viewModel);
        }

        [HttpDelete]
        public async Task<IActionResult> RemovePicture(int productId, int pictureId)
        {
            var product = await _productServices.GetByIdAsync(productId);
            if (product == null || !IsAjax())
            {
                return BadRequest();
            }

            var picture = product.Pictures.FirstOrDefault(x => x.Id == pictureId);
            if (picture == null)
            {
                return BadRequest();
            }

            await _productServices.DeletePictureAsync(picture, Messages);
            return Ok();
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _productServices.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return BadRequest();
            }

            if (!await _productServices.DeleteCategoryAsync(category, Messages))
            {
                return RedirectToAction(nameof(EditCategory), new { id = category.Id });
            }

            return RedirectToAction(nameof(Categories));
        }

        [HttpGet]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(ImportProductsRequestModel model)
        {
            if (!ModelState.IsValid || model.File == null || model.File.Length == 0)
            {
                return RedirectToAction(nameof(Import));
            }

            using (var memoryStream = new MemoryStream())
            {
                model.File.CopyTo(memoryStream);
                memoryStream.Position = 0;
                var productDtos = new List<ProductDto>();

                using (var reader = new StreamReader(memoryStream))
                {
                    string json = reader.ReadToEnd();
                    productDtos = JsonConvert.DeserializeObject<List<ProductDto>>(json);
                }

                await _productServices.ImportAsync(productDtos, Messages);
            }

            //return ViewComponent()
            return RedirectToAction(nameof(Import));
        }

        [HttpGet]
        public async Task<IActionResult> Priority(int id)
        {
            var product = await _productServices.GetByIdAsync(id);
            if (product == null)
            {
                return BadRequest();
            }

            var priorities = await _priorityServices.GetListAsync(id);
            var user = await _authenticationServices.GetCurrentUserAsync();
            var priority = priorities.FirstOrDefault(x => x.User == user);

            var viewModel = new PriorityViewModel()
            {
                Id = product.Id,
                PicturesCount = product.Pictures.Count,
                Priority = priority,
                Priorities = priorities,
                PriorityItems = Enum.GetValues(typeof(ProductPriorityEnum)).Cast<ProductPriorityEnum>().ToSelectList(x => ((int)x).ToString(), x => x.ToString(), priority?.Id.ToString() ?? ((int)ProductPriorityEnum.Normal).ToString(), false)
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Priority(PriorityRequestModel model)
        {
            var product = await _productServices.GetByIdAsync(model.Id);
            var user = await _authenticationServices.GetCurrentUserAsync();

            if (product == null || user == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ModelState.GetErrors().ForEach(x => Messages.AddWarning(x));
            }
            else
            {
                var priority = (await _priorityServices.GetListAsync(model.Id, x => x.User == user)).FirstOrDefault();
                if (priority != null)
                {
                    priority.Priority = (ProductPriorityEnum)model.Priority;
                }
                else
                {
                    priority = new ProductPriority()
                    {
                        Priority = (ProductPriorityEnum)model.Priority,
                        User = user,
                        Product = product
                    };
                }

                if (await _priorityServices.EditAsync(priority, Messages))
                {
                    Messages.AddSuccess("Priority Edited");
                }
            }

            return RedirectToAction(nameof(Priority), new { id = product?.Id });
        }

        [HttpGet]
        public async Task<IActionResult> History(int id, int page)
        {
            var product = await _productServices.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewData["PictureCount"] = product.Pictures.Count;
            var memos = await _memoServices.GetMemosAsync(product.Id, product.GetType().Name, page, 10);
            var viewModel = new Administration.Presenters.Dtos.HistoryViewModel
            {
                Id = product.Id,
                Memos = memos
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productServices.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            if (await _productServices.DeleteAsync(product, Messages))
            {
                Messages.AddSuccess("Product deleted");
            }
            else
            {
                Messages.AddError("Cannot delete product");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            var settings = await _productServices.GetSettingsAsync();
            var viewModel = Mapper.Map<SettingsViewModel>(settings);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Settings(SettingsRequestModel model)
        {
            var settings = await _productServices.GetSettingsAsync();

            if (!ModelState.IsValid)
            {
                ModelState.GetErrors().ForEach(x => Messages.AddWarning(x));
            }
            else
            {
                settings = Mapper.Map(model, settings);
                if (await _productServices.EditSettingsAsync(settings, Messages))
                {
                    Messages.AddSuccess("Settings Edited");
                }
            }

            return RedirectToAction(nameof(Settings));
        }
    }
}
