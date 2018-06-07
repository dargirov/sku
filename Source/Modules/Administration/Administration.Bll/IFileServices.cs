using Administration.Entities;
using Infrastructure.Data.Common;
using System;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public interface IFileServices
    {
        Task<File> GetAsync(Guid guid);
        Task<bool> SaveAsync(File file, Messages messages);
        Task<File> GetByIdAsync(int id);
    }
}
