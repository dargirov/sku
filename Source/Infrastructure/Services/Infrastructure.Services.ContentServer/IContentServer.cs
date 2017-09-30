using Administration.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Infrastructure.Services.ContentServer
{
    public interface IContentServer
    {
        Task<File> SaveAsync(IFormFile formFile);

        Task<File> ResizeAndSaveAsync(IFormFile file);

        Task<byte[]> GetAsync(File file);
    }
}
