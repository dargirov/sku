using Administration.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Infrastructure.Services.ContentServer
{
    public class ContentServerContext
    {
        private StorageClient storageClient;

        public ContentServerContext(StorageClient storage)
        {
            storageClient = storage;
        }

        public async Task<File> SaveAsync(IFormFile file)
        {
            return await storageClient.SaveAsync(file);
        }

        public async Task<File> ResizeAndSaveAsync(IFormFile file, int width, int height)
        {
            return await storageClient.ResizeAndSaveAsync(file, width, height);
        }

        public async Task<byte[]> GetAsync(File file)
        {
            return await storageClient.GetAsync(file);
        }
    }
}
