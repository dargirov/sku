using Administration.Bll;
using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using Microsoft.EntityFrameworkCore;
using Request.Bll.Dtos;
using Store.Bll;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Request.Bll
{
    public class RequestServices : IRequestServices
    {
        private readonly IRepository _repository;
        private readonly IEntityServices _entityServices;
        private readonly IAuthorizationServices _authorizationServices;
        private readonly IStoreServices _storeServices;

        public RequestServices(IRepository repository, IEntityServices entityServices, IAuthorizationServices authorizationServices, IStoreServices storeServices)
        {
            _repository = repository;
            _entityServices = entityServices;
            _authorizationServices = authorizationServices;
            _storeServices = storeServices;
        }

        public async Task<Entities.Request> GetByIdAsync(int id)
        {
            var privs = await _authorizationServices.GetModulePrivileges();
            if (!privs.RequestRead)
            {
                return null;
            }

            var request = await _repository.GetQueryable<Entities.Request>()
                .Include(x => x.StockRequests)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (request == null)
            {
                return null;
            }

            request.StockRequests = await _repository.GetQueryable<Entities.StockRequest>()
                .Include(x => x.FromStore)
                .Include(x => x.ToStore)
                .Include(x => x.Stock)
                .ThenInclude(x => x.Variant)
                .ThenInclude(x => x.Stocks)
                .ThenInclude(x => x.Variant)
                .ThenInclude(x => x.Product)
                .Where(x => x.Request == request)
                .ToListAsync();

            return request;
        }

        public async Task<bool> EditAsync(Entities.Request request, Messages messages)
        {
            var privs = await _authorizationServices.GetModulePrivileges();
            if (!privs.RequestWrite)
            {
                return false;
            }

            return await _entityServices.SaveAsync<Entities.Request>(request, messages);
        }

        public async Task<bool> EditAsync(Entities.Request request, IEnumerable<StockRequestDto> stockRequestDtos, Messages messages)
        {
            var privs = await _authorizationServices.GetModulePrivileges();
            if (!privs.RequestWrite)
            {
                return false;
            }

            foreach (var stockRequestDto in stockRequestDtos)
            {
                var stockRequest = request.StockRequests.FirstOrDefault(x => x.StockId == stockRequestDto.StockId);
                if (stockRequest == null)
                {
                    stockRequest = new Entities.StockRequest() { StockId = stockRequestDto.StockId };
                    request.StockRequests.Add(stockRequest);
                }

                stockRequest.FromStoreId = stockRequestDto.FromStoreId;
                stockRequest.ToStoreId = stockRequestDto.ToStoreId;
                stockRequest.Quantity = stockRequestDto.Quantity;
                stockRequest.Priority = stockRequestDto.Priority;
            }

            return await _entityServices.SaveAsync<Entities.Request>(request, messages);
        }

        public async Task<(IEnumerable<Entities.Request> requests, PageData pageData)> GetListAsync(int page, int pageSize, int column, SortDirectionEnum dir)
        {
            var privs = await _authorizationServices.GetModulePrivileges();
            var storeIds = (await _storeServices.GetListAsync()).Select(x => x.Id);

            var query = _repository.GetQueryable<Entities.Request>()
                .Include(x => x.StockRequests)
                .Where(x => x.StockRequests.Any(r => storeIds.Contains(r.FromStoreId) && storeIds.Contains(r.ToStoreId)))
                .OrderByDescending(x => x.Id);

            if (!privs.RequestRead)
            {
                return query.ToEmptyListWithPageData();
            }

            return await query.ToListWithPageData(page, pageSize);
        }

        public async Task<(IEnumerable<Entities.Request> requests, PageData pageData)> GetListAsync(int page, int pageSize, params Entities.RequestStatusEnum[] statuses)
        {
            // TODO: return requests for stores that the user has pruvs on !!!
            var privs = await _authorizationServices.GetModulePrivileges();

            var query = _repository.GetQueryable<Entities.Request>()
                .Include(x => x.StockRequests)
                .Where(x => statuses.Contains(x.Status))
                .OrderByDescending(x => x.Id);

            if (!privs.RequestRead)
            {
                return query.ToEmptyListWithPageData();
            }

            return await query.ToListWithPageData(page, pageSize);
        }

        public async Task<Entities.StockRequest> GetStockRequestByIdAsync(int id)
        {
            var privs = await _authorizationServices.GetModulePrivileges();
            if (!privs.RequestRead)
            {
                return null;
            }

            return await _repository.GetQueryable<Entities.StockRequest>()
                .Where(x => x.Id == id)
                .Include(x => x.Request)
                .Include(x => x.FromStore)
                .Include(x => x.ToStore)
                .Include(x => x.Stock)
                .ThenInclude(x => x.Variant)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteStockRequestAsync(Entities.StockRequest stockRequest, Messages messages)
        {
            var privs = await _authorizationServices.GetModulePrivileges();
            if (!privs.RequestDelete)
            {
                return false;
            }

            return await _entityServices.DeleteAsync<Entities.StockRequest>(stockRequest, messages);
        }

        public Entities.RequestStatusEnum? GetNextStatus(Entities.RequestStatusEnum status)
        {
            Entities.RequestStatusEnum? newStatus = null;

            switch (status)
            {
                case Entities.RequestStatusEnum.New:
                    newStatus = Entities.RequestStatusEnum.Sent;
                    break;
                case Entities.RequestStatusEnum.Sent:
                case Entities.RequestStatusEnum.PartiallyCompleted:
                    newStatus = Entities.RequestStatusEnum.Completed;
                    break;
                case Entities.RequestStatusEnum.Completed:
                    newStatus = Entities.RequestStatusEnum.Cancelled;
                    break;
            }

            return newStatus;
        }

        public async Task<bool> EditProductsQuantityAsync(Entities.Request request, IEnumerable<StockRequestDto> stockRequestDtos, Messages messages)
        {
            var privs = await _authorizationServices.GetModulePrivileges();
            var storeWriteIds = (await _storeServices.GetStoreListWithWritePrivilegeAsync()).Select(x => x.Id);

            if (!privs.RequestWrite)
            {
                return false;
            }

            foreach (var stockRequestDto in stockRequestDtos)
            {
                var stockRequest = request.StockRequests.First(x => x.StockId == stockRequestDto.StockId);
                var fromStock = stockRequest.Stock.Variant.Stocks.First(x => x.Store == stockRequest.FromStore);
                var toStock = stockRequest.Stock.Variant.Stocks.FirstOrDefault(x => x.Store == stockRequest.ToStore);
                if (toStock == null)
                {
                    toStock = new Product.Entities.ProductStock()
                    {
                        LowQuantity = fromStock.LowQuantity,
                        LowQuantityMeasureType = fromStock.LowQuantityMeasureType,
                        Quantity = 0,
                        QuantityMeasureType = fromStock.QuantityMeasureType,
                        Store = stockRequest.ToStore,
                        Variant = stockRequest.Stock.Variant
                    };
                }

                if (!storeWriteIds.Contains(fromStock.StoreId))
                {
                    messages.AddError($"No write priv for store {fromStock.Store.Name}");
                    return false;
                }

                if (!storeWriteIds.Contains(toStock.Store.Id))
                {
                    messages.AddError($"No write priv for store {toStock.Store.Name}");
                    return false;
                }

                var quantityChange = fromStock.Quantity >= stockRequest.Quantity ? stockRequest.Quantity : fromStock.Quantity;
                fromStock.Quantity -= quantityChange;
                toStock.Quantity += quantityChange;
                stockRequest.Quantity -= quantityChange;

                using (var transaction = await _repository.BeginTransactionAsync())
                {
                    if (!await _entityServices.SaveAsync<Product.Entities.ProductStock>(fromStock, messages)
                        || !await _entityServices.SaveAsync<Product.Entities.ProductStock>(toStock, messages)
                        || !await _entityServices.SaveAsync<Entities.StockRequest>(stockRequest, messages))
                    {
                        return false;
                    }

                    transaction.Commit();
                }
            }

            return true;
        }
    }
}
