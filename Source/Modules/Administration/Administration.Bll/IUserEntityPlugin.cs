using Administration.Entities;
using Infrastructure.Data.Common;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public interface IUserEntityPlugin
    {
        Task<bool> OnDelete(User user, Messages messages);
    }
}
