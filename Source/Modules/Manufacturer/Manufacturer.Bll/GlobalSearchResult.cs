using Administration.Bll;
using Administration.Bll.Dtos;
using Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manufacturer.Bll
{
    public class GlobalSearchResult : IGlobalSearchResult
    {
        private readonly IRepository _repository;
        private readonly IAuthenticationServices _authenticationServices;

        public GlobalSearchResult(IRepository repository, IAuthenticationServices authenticationServices)
        {
            _repository = repository;
            _authenticationServices = authenticationServices;
        }

        public async Task<IList<GlobalSearchResultDto>> GetResults(string search)
        {
            var result = new List<GlobalSearchResultDto>();
            var user = await _authenticationServices.GetCurrentUser();

            if (!user.IsAdmin && !user.ModulePrivilege.ManufacturerRead)
            {
                return result;
            }

            var manufacturers = await _repository.GetQueryable<Entities.Manufacturer, int>()
                .Include(x => x.Country)
                .Where(x => x.Name.Contains(search))
                .ToListAsync();

            foreach (var manufacturer in manufacturers)
            {
                result.Add(new GlobalSearchResultDto()
                {
                    Id = manufacturer.Id,
                    Title = manufacturer.Name,
                    Details1 = string.Join(", ", manufacturers.Select(x => x.Country.Name)),
                    Type = nameof(Entities.Manufacturer)
                });
            }

            return result;
        }
    }
}
