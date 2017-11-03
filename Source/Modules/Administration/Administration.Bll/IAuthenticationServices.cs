using Administration.Entities;
using Infrastructure.Data.Common;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public interface IAuthenticationServices
    {
        Task<bool> LoginAsync(string email, string password, Messages messages);
        void Logout();
        Task<User> GetCurrentUserAsync();
    }
}
