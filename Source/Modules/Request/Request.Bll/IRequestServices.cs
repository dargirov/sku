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
        Task<bool> EditAsync(Entities.Request request, Messages messages);
        Task<bool> EditAsync(Entities.Request request, IEnumerable<StockRequestDto> stockRequestDtos, Messages messages);
        Task<(IEnumerable<Entities.Request> requests, PageData pageData)> GetListAsync(int page, int pageSize, int column, SortDirectionEnum dir);
        Task<bool> DeleteStockRequestAsync(Entities.StockRequest stockRequest, Messages messages);
        Entities.RequestStatusEnum? GetNextStatus(Entities.RequestStatusEnum status);
        Task<(IEnumerable<Entities.Request> requests, PageData pageData)> GetListAsync(int page, int pageSize, params Entities.RequestStatusEnum[] statuses);
        Task<bool> EditProductsQuantityAsync(Entities.Request request, IEnumerable<StockRequestDto> stockRequestDtos, Messages messages);
    }
}
