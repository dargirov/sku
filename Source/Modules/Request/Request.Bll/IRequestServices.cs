using Infrastructure.Data.Common;
using Request.Bll.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Request.Bll
{
    public interface IRequestServices
    {
        Task<Entities.Request> GetByIdAsync(int id);
        Task<Entities.StockRequest> GetStockRequestByIdAsync(int id);
        Task<int> EditAsync(Entities.Request request);
        Task<int> EditAsync(Entities.Request request, IEnumerable<StockRequestDto> stockRequestDtos);
        Task<(IEnumerable<Entities.Request> requests, PageData pageData)> GetListAsync(int page, int pageSize, int column, SortDirectionEnum dir);
        Task<int> DeleteStockRequestAsync(Entities.StockRequest stockRequest);
        Entities.RequestStatusEnum? GetNextStatus(Entities.RequestStatusEnum status);
        Task<(IEnumerable<Entities.Request> requests, PageData pageData)> GetListAsync(int page, int pageSize, params Entities.RequestStatusEnum[] statuses);
    }
}
