using Administration.Entities;
using Infrastructure.Data.Common;
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
        Task<bool> EditAsync(User user, Messages messages);
        Task<bool> EditAsync(User user, bool hashPassword, Messages messages);
        Task<bool> DeleteAsync(User user, Messages messages);
    }
}