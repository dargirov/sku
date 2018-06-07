using Infrastructure.Data.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public interface IMemoServices
    {
        Task<(IEnumerable<Entities.Memo> memos, PageData pageData)> GetMemosAsync(int entityId, string entityName, int page, int pageSize);
    }
}