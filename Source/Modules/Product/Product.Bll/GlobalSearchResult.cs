using Administration.Bll;
using Administration.Bll.Dtos;
using Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Store.Bll;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Bll
{
    public class GlobalSearchResult : IGlobalSearchResult
    {
        private readonly IRepository _repository;
        private readonly IStoreServices _storeServices;

        public GlobalSearchResult(IRepository repository, IStoreServices storeServices)
        {
            _repository = repository;
            _storeServices = storeServices;
        }

        public async Task<IList<GlobalSearchResultDto>> GetResults(string search)
        {
            var storeIds = (await _storeServices.GetListAsync()).Select(x => x.Id);

            var products = await _repository.GetQueryable<Entities.Product, int>()
               .Include(x => x.Pictures).ThenInclude(x => x.FullSize)
               .Include(x => x.Pictures).ThenInclude(x => x.Thumb)
               .Include(x => x.Variants).ThenInclude(x => x.Stocks)
               .Where(x => x.Variants.Any(v => v.Stocks.Any(s => storeIds.Contains(s.StoreId))))
               .Where(x => x.Name.Contains(search) || x.Variants.Any(v => v.Code.Contains(search)))
               .ToListAsync();

            var result = new List<GlobalSearchResultDto>();
            foreach (var product in products)
            {
                result.Add(new GlobalSearchResultDto()
                {
                    Id = product.Id,
                    Title = product.Name,
                    Details1 = string.Join(", ", product.Variants.Select(x => x.Code)),
                    Type = nameof(Entities.Product),
                    Picture = product.Pictures.FirstOrDefault()?.Thumb.Guid.ToString()
                });
            }

            return result;
        }
    }
}
