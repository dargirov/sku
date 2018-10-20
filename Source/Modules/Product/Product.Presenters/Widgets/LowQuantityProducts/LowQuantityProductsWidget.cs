using Administration.Bll;
using Administration.Entities;
using Infrastructure.Database.Repository;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Bll;
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

        public async Task<IViewComponentResult> InvokeAsync(string page, int limit, string checkedStoreIdsAsString)
        {
            int currentPage = Parse.TryParseInt(page) ?? 1;

            var checkedStoreIds = checkedStoreIdsAsString?.Split(",").Select(x => Parse.TryParseInt(x) ?? 0).Where(x => x > 0).ToList() ?? Enumerable.Empty<int>();

            var stores = await _storeServices.GetStoreListWithReadPrivilegeAsync();
            var storeIds = checkedStoreIds.Any()
                ? stores.Select(x => x.Id).Intersect(checkedStoreIds)
                : stores.Select(x => x.Id);

            var productIds = await _repository.GetQueryable<Entities.Product>()
                .Where(x => x.Variants.Any(v => v.Stocks.Any(s => storeIds.Contains(s.StoreId) && s.Quantity < s.LowQuantity)))
                .Select(x => x.Id)
                .ToListAsync();

            var products = await _repository.GetQueryable<Entities.Product>()
                .Include(x => x.Variants)
                .ThenInclude(x => x.Stocks)
                .ThenInclude(x => x.Store)
                .Where(x => productIds.Contains(x.Id))
                .Skip((currentPage - 1) * limit)
                .Take(limit)
                .ToListAsync();

            var productDtos = new List<ProductDto>();

            foreach (var product in products)
            {
                var stocks = product.Variants
                    .Where(v => v.Stocks.Any(s => storeIds.Contains(s.StoreId) && s.Quantity < s.LowQuantity))
                    .SelectMany(x => x.Stocks.Where(s => s.Quantity < s.LowQuantity))
                    .Where(x => storeIds.Contains(x.StoreId))
                    .OrderBy(x => x.Id);

                var memos = await _repository.GetQueryable<Memo>()
                    .Where(x => x.BaseEntityId == product.Id
                        && x.BaseEntityName == nameof(Entities.Product)
                        && stocks.Select(s => s.Id).Contains(x.EntityId)
                        && x.EntityName == nameof(Entities.ProductStock))
                    .OrderBy(x => x.EntityId)
                    .ToListAsync();

                var dto = new ProductDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    StoreNames = stocks.Select(x => x.Store.Name),
                    VariantNames = stocks.Select(x => x.Variant.Code),
                    Quantities = stocks.Select(x => x.Quantity),
                    LowQuantities = stocks.Select(x => x.LowQuantity),
                    Dates = stocks.Select(x => memos.FirstOrDefault(m => m.EntityId == x.Id)?.CreatedOn)
                };

                productDtos.Add(dto);
            }

            var viewModel = new ViewModel()
            {
                Stores = stores.Select(x => new StoreDto() { Id = x.Id, Name = x.Name, Checked = !checkedStoreIds.Any() || checkedStoreIds.Contains(x.Id) }),
                Products = productDtos,
                PageData = new Infrastructure.Data.Common.PageData(productIds.Count, currentPage, limit)
            };

            return View(viewModel);
        }
    }
}
