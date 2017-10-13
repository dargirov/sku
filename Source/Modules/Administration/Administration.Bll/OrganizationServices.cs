using Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public class OrganizationServices : IOrganizationServices
    {
        private readonly IRepository _repository;

        public OrganizationServices(IRepository repository)
        {
            _repository = repository;
        }

        public Task<Entities.Organization> GetByNameAsync(string name)
        {
            return _repository.GetQueryable<Entities.Organization, int>()
                .Where(x => x.Name == name.Trim())
                .FirstOrDefaultAsync();
        }
    }
}
