using Infrastructure.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services.Common
{
    public static class QueryableExtensions
    {
        private const int _defaultPage = 1;
        private const int _defaultPageSize = 10;

        public static async Task<(IEnumerable<T> results, PageData pageData)> ToListWithPageData<T>(this IQueryable<T> query, int page, int pageSize)
        {
            var realPage = page > 0 ? page : _defaultPage;
            var realPageSize = pageSize > 0 ? pageSize : _defaultPageSize;

            var results = await query.Skip((realPage - 1) * realPageSize).Take(realPageSize).ToListAsync();
            var resultCount = await query.CountAsync();

            var pageData = new PageData(resultCount, realPage, realPageSize);

            return (results: results, pageData: pageData);
        }

        public static (IEnumerable<T> results, PageData pageData) ToEmptyListWithPageData<T>(this IQueryable<T> query)
        {
            var pageData = new PageData(0, 1, _defaultPageSize);

            return (results: Enumerable.Empty<T>(), pageData: pageData);
        }
    }
}
