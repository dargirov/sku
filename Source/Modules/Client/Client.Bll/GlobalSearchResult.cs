using Administration.Bll;
using Administration.Bll.Dtos;
using Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Bll
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
            var user = await _authenticationServices.GetCurrentUserAsync();

            if (!user.IsAdmin && !user.ModulePrivilege.ClientRead)
            {
                return result;
            }

            var naturalClients = await _repository.GetQueryable<Entities.NaturalClient, int>()
                .Where(x => x.Name.Contains(search) || x.PersonalNo.Contains(search))
                .ToListAsync();

            var legalClients = await _repository.GetQueryable<Entities.LegalClient, int>()
                .Where(x => x.FirmName.Contains(search) || x.Mol.Contains(search) || x.Phone.Contains(search) || x.Eik.Contains(search))
                .ToListAsync();

            foreach (var client in naturalClients)
            {
                result.Add(new GlobalSearchResultDto()
                {
                    Id = client.Id,
                    Title = client.Name,
                    Details1 = GetNaturalClientDetails(client),
                    Type = nameof(Entities.NaturalClient),
                });
            }

            foreach (var client in legalClients)
            {
                result.Add(new GlobalSearchResultDto()
                {
                    Id = client.Id,
                    Title = client.FirmName,
                    Details1 = GetLegalClientDetails(client),
                    Type = nameof(Entities.LegalClient)
                });
            }

            string GetNaturalClientDetails(Entities.NaturalClient client)
            {
                var data = new List<string>() { client.PersonalNo };
                if (client.Phone != null)
                {
                    data.Add(client.Phone);
                }

                return string.Join(" - ", data);
            }

            string GetLegalClientDetails(Entities.LegalClient client)
            {
                return string.Join(" - ", new string[] { client.Eik, client.Mol });
            }

            return result;
        }
    }
}
