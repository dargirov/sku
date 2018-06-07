using Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public class CountryServices : ICountryServices
    {
        private readonly IRepository _repository;

        public CountryServices(IRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Entities.Country>> GetListAsync()
        {
            return _repository.GetQueryable<Entities.Country>()
                .OrderByDescending(c => c.Highlight)
                .ThenBy(c => c.Name)
                .ToListAsync();
        }
    }
}
