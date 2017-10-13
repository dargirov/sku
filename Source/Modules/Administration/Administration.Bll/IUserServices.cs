using Administration.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public interface IUserServices
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByEmailAndPasswordAsync(string email, string password);
        Task<List<User>> GetListAsync();
        Task<int> EditAsync(User user);
        Task<int> EditAsync(User user, bool hashPassword);
    }
}