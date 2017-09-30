using Administration.Entities;
using Infrastructure.Database.Repository;
using Infrastructure.Services.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public class FileServices : IFileServices
    {
        private readonly IRepository _repository;
        private readonly IEntityServices _entityServices;

        public FileServices(IRepository repository, IEntityServices entityServices)
        {
            _repository = repository;
            _entityServices = entityServices;
        }

        public Task<File> GetAsync(Guid guid)
        {
            return _repository.GetQueryable<File, int>()
                .Where(x => x.Guid == guid)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveAsync(File file)
        {
            return _entityServices.SaveAsync<File, int>(file);
        }

        public Task<File> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync<File, int>(id);
        }
    }
}
