using Administration.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Infrastructure.Services.ContentServer
{
    public abstract class StorageClient
    {
        public abstract Task<File> SaveAsync(IFormFile formFile);

        public abstract Task<File> ResizeAndSaveAsync(IFormFile file, int width, int height);

        public abstract Task<byte[]> GetAsync(File file);
    }
}
