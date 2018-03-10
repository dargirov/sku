using Administration.Bll;
using Infrastructure.Data.Common;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;
using Product.Bll;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Presenters.Widgets.LatestProducts
{
    public class LatestProductsWidget : ViewComponent, IWidget
    {
        private readonly IProductServices _productServices;

        public LatestProductsWidget(IProductServices productServices)
        {
            _productServices = productServices;
        }

        public string Name => nameof(LatestProductsWidget);

        public async Task<IViewComponentResult> InvokeAsync(string page, int limit)
        {
            int currentPage = Parse.TryParseInt(page) ?? 1;

            var (products, count) = (await _productServices.GetListAsync((currentPage - 1) * limit, limit, "desc"));
            var pageCount = (int)Math.Ceiling((decimal)count / limit);

            var viewModel = new ViewModel()
            {
                Products = products.Select(x => new ProductDto()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Category = x.Category.Name,
                        CreatedOn = x.CreatedOn,
                        ManufacturerId = x.ManufacturerId,
                        Manufacturer = x.Manufacturer.Name,
                        VariantCount = x.Variants.Count,
                        TotalStockCount = x.Variants.SelectMany(y => y.Stocks).Sum(y => y.Quantity)
                    }),
                PageSortData = new PageSortData()
                {
                    Page = currentPage,
                    TotalPages = pageCount > 10 ? 10 : pageCount,
                    PageSize = limit
                }
            };

            return View(viewModel);
        }
    }
}
