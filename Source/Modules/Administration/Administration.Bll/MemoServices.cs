using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public class MemoServices : IMemoServices
    {
        private readonly IRepository _repository;

        public MemoServices(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<(IEnumerable<Entities.Memo> memos, PageData pageData)> GetMemosAsync(int entityId, string entityName, int page, int pageSize)
        {
            var query = _repository.GetQueryable<Entities.Memo>()
                .Where(x => x.BaseEntityId == entityId && x.BaseEntityName == entityName)
                .OrderByDescending(x => x.ChangedOn);

            return await query.ToListWithPageData(page, pageSize);
        }
    }
}
