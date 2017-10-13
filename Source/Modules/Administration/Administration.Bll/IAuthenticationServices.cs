using Administration.Entities;
using Infrastructure.Data.Common;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public interface IAuthenticationServices
    {
        Task<bool> Login(string email, string password, Messages messages);
        Task<User> GetCurrentUser();
    }
}
