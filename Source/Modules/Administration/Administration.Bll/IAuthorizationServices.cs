using Administration.Entities;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public interface IAuthorizationServices
    {
        Task<ModulePrivilege> GetModulePrivileges();
    }
}
