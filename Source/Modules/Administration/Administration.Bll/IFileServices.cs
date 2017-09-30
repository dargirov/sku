using Administration.Entities;
using System;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public interface IFileServices
    {
        Task<File> GetAsync(Guid guid);
        Task<int> SaveAsync(File file);
        Task<File> GetByIdAsync(int id);
    }
}
