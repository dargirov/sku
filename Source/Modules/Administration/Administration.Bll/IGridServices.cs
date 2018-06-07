using Infrastructure.Data.Common;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public interface IGridServices
    {
        Task<int> GetPageSizeAsync(string gridId);
        Task<int> UpdateAndGetPageSizeAsync(string gridId, int newPageSize, Messages messages);
    }
}
