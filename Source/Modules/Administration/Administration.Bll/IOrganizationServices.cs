using System.Threading.Tasks;

namespace Administration.Bll
{
    public interface IOrganizationServices
    {
        Task<Entities.Organization> GetByNameAsync(string name);
    }
}
