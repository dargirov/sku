using Administration.Bll;
using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Store.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Presenters.Widgets.LowQuantityProducts
{
    public class LowQuantityProductsWidget : ViewComponent, IWidget
    {
        private readonly IStoreServices _storeServices;
        private readonly IRepository _repository;

        public LowQuantityProductsWidget(IStoreServices storeServices, IRepository repository)
        {
            _storeServices = storeServices;
            _repository = repository;
        }

        public string Name => nameof(LowQuantityProductsWidget);

        public async Task<IViewComponentResult> InvokeAsync(string page, int limit)
        {
            int currentPage = Parse.TryParseInt(page) ?? 1;

            var stores = await _storeServices.GetStoreListWithReadPrivilegeAsync();
            var storeIds = stores.Select(x => x.Id);

            var productIds = await _repository.GetQueryable<Entities.Product, int>()
                .Where(x => x.Variants.Any(v => v.Stocks.Any(s => storeIds.Contains(s.StoreId) && s.Quantity < s.LowQuantity)))
                .Select(x => x.Id)
                .ToListAsync();

            var products = await _repository.GetQueryable<Entities.Product, int>()
                .Include(x => x.Variants)
                .ThenInclude(x => x.Stocks)
                .Where(x => productIds.Contains(x.Id))
                .Skip((currentPage - 1) * limit)
                .Take(limit)
                .ToListAsync();

            var pageCount = (int)Math.Ceiling((decimal)productIds.Count / limit);

            var productDtos = new List<ProductDto>();

            foreach (var product in products)
            {
                var stock = product.Variants.First(v => v.Stocks.Any(s => s.Quantity < s.LowQuantity)).Stocks.First(s => s.Quantity < s.LowQuantity);
                var dto = new ProductDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    StoreName = stores.First(x => x.Id == stock.StoreId).Name,
                    Quantity = stock.Quantity,
                    LowQuantity = stock.LowQuantity
                };

                productDtos.Add(dto);
            }

            var viewModel = new ViewModel()
            {
                Products = productDtos,
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
