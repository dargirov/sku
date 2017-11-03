using Administration.Entities;
using Infrastructure.Database.Repository;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public class AuthorizationServices : IAuthorizationServices
    {
        private readonly IRepository _repository;
        private readonly IAuthenticationServices _authenticationServices;

        public AuthorizationServices(IRepository repository,IAuthenticationServices authenticationServices)
        {
            _repository = repository;
            _authenticationServices = authenticationServices;
        }

        public async Task<ModulePrivilege> GetModulePrivileges()
        {
            return (await _authenticationServices.GetCurrentUserAsync()).ModulePrivilege;
        }
    }
}
