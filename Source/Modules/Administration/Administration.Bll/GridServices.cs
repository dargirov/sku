using Infrastructure.Data.Common;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public class GridServices : IGridServices
    {
        private readonly IRepository _repository;
        private readonly IAuthenticationServices _authenticationServices;
        private readonly IEntityServices _entityServices;
        private readonly int[] _allowedPageSize = { 10, 20, 50, 100 };
        private const int _defaultPageSize = 10;

        public GridServices(IRepository repository, IAuthenticationServices authenticationServices, IEntityServices entityServices)
        {
            _repository = repository;
            _authenticationServices = authenticationServices;
            _entityServices = entityServices;
        }

        public async Task<int> UpdateAndGetPageSizeAsync(string gridId, int newPageSize, Messages messages)
        {
            var currentUser = await _authenticationServices.GetCurrentUserAsync();
            if (newPageSize != default(int) && !_allowedPageSize.Contains(newPageSize))
            {
                return _defaultPageSize;
            }

            var pageData = _repository.GetQueryable<Entities.PageData>()
                .Where(x => x.GridId == gridId && x.User == currentUser)
                .FirstOrDefault();

            var save = false;
            if (pageData == null)
            {
                save = true;
                pageData = new Entities.PageData()
                {
                    GridId = gridId,
                    PageSize = newPageSize == default(int) ? _defaultPageSize : newPageSize,
                    User = currentUser
                };
            }
            else if (newPageSize != default(int) && newPageSize != pageData.PageSize)
            {
                save = true;
                pageData.PageSize = newPageSize;
            }

            if (save && !await _entityServices.SaveAsync(pageData, messages))
            {
                return _defaultPageSize;
            }

            return pageData.PageSize;
        }

        public async Task<int> GetPageSizeAsync(string gridId)
        {
            var currentUser = await _authenticationServices.GetCurrentUserAsync();

            return _repository.GetQueryable<Entities.PageData>()
                .Where(x => x.GridId == gridId && x.User == currentUser)
                .FirstOrDefault()?.PageSize ?? _defaultPageSize;
        }
    }
}
