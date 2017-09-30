using Infrastructure.Database.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public class CityServices : ICityServices
    {
        private readonly IRepository _repository;

        public CityServices(IRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Entities.City>> GetListAsync()
        {
            return _repository.GetListAsync<Entities.City, int>();
        }
    }
}
