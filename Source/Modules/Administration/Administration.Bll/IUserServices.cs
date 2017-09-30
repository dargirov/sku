using Administration.Entities;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public interface IUserServices
    {
        Task<User> GetTestAsync(int id);
    }
}