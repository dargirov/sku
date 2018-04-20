using Administration.Bll;
using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using Microsoft.EntityFrameworkCore;
using Request.Bll.Dtos;
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

        public RequestServices(IRepository repository, IEntityServices entityServices, IAuthorizationServices authorizationServices)
        {
            _repository = repository;
            _entityServices = entityServices;
            _authorizationServices = authorizationServices;
        }

        public async Task<Entities.Request> GetByIdAsync(int id)
        {
            var privs = await _authorizationServices.GetModulePrivileges();
            if (!privs.RequestRead)
            {
                return null;
            }

            var request = await _repository.GetQueryable<Entities.Request, int>()
                .Include(x => x.StockRequests)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (request == null)
            {
                return null;
            }

            request.StockRequests = await _repository.GetQueryable<Entities.StockRequest, int>()
                .Include(x => x.FromStore)
                .Include(x => x.ToStore)
                .Include(x => x.Stock)
                .ThenInclude(x => x.Variant)
                .ThenInclude(x => x.Product)
                .Where(x => x.Request == request)
                .ToListAsync();

            return request;
        }

        public async Task<int> EditAsync(Entities.Request request)
        {
            var privs = await _authorizationServices.GetModulePrivileges();
            if (!privs.RequestWrite)
            {
                return default(int);
            }

            return await _entityServices.SaveAsync<Entities.Request, int>(request);
        }

        public async Task<int> EditAsync(Entities.Request request, IEnumerable<StockRequestDto> stockRequestDtos)
        {
            var privs = await _authorizationServices.GetModulePrivileges();
            if (!privs.RequestWrite)
            {
                return default(int);
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

            return await _entityServices.SaveAsync<Entities.Request, int>(request);
        }

        public async Task<(IEnumerable<Entities.Request> requests, PageData pageData)> GetListAsync(int page, int pageSize, int column, SortDirectionEnum dir)
        {
            // TODO: return requests for stores that the user has pruvs on !!!
            var privs = await _authorizationServices.GetModulePrivileges();

            var query = _repository.GetQueryable<Entities.Request, int>()
                .Include(x => x.StockRequests)
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

            var query = _repository.GetQueryable<Entities.Request, int>()
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

            return await _repository.GetQueryable<Entities.StockRequest, int>()
                .Where(x => x.Id == id)
                .Include(x => x.Request)
                .Include(x => x.FromStore)
                .Include(x => x.ToStore)
                .Include(x => x.Stock)
                .ThenInclude(x => x.Variant)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync();
        }

        public async Task<int> DeleteStockRequestAsync(Entities.StockRequest stockRequest)
        {
            var privs = await _authorizationServices.GetModulePrivileges();
            if (!privs.RequestDelete)
            {
                return default(int);
            }

            return await _entityServices.DeleteAsync<Entities.StockRequest, int>(stockRequest);
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
    }
}
