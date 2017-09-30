using Administration.Entities;
using Infrastructure.Database.Repository;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public class UserServices : IUserServices
    {
        private readonly IRepository _repository;

        public UserServices(IRepository repository)
        {
            _repository = repository;
        }

        public Task<User> GetTestAsync(int id)
        {
            return _repository.GetByIdAsync<User, int>(id);
        }
    }
}
