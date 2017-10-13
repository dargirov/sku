using Administration.Entities;
using ImageSharp;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services.ContentServer
{
    public class AzureStorageClient : StorageClient
    {
        private CloudStorageAccount StorageAccount { get; set; }
        private string ContainerName { get; set; }
        private User User { get; set; }

        public AzureStorageClient(User user, string accountName, string accessKey)
        {
            User = user;
            StorageAccount = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(accountName, accessKey), true);
            ContainerName = $"organization{user.OrganizationId}";
        }

        public override async Task<Administration.Entities.File> SaveAsync(IFormFile formFile)
        {
            var info = GetFileInfo(formFile);

            var container = GetContainer(ContainerName);
            if (!await container.ExistsAsync())
            {
                container = await CreateContainer(ContainerName);
            }

            var blockBlob = container.GetBlockBlobReference(info.name);
            using (var fileStream = formFile.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(fileStream);
            }

            return SetupFile(formFile, info);
        }

        public override async Task<Administration.Entities.File> ResizeAndSaveAsync(IFormFile formFile, int width, int height)
        {
            var info = GetFileInfo(formFile);

            var container = GetContainer(ContainerName);
            if (!await container.ExistsAsync())
            {
                container = await CreateContainer(ContainerName);
            }

            var blockBlob = container.GetBlockBlobReference(info.name);

            using (var readStream = formFile.OpenReadStream())
            {
                var image = new Image(readStream);

                using (var memoryStream = new MemoryStream())
                {
                    image.Resize(width, height).Save(memoryStream);
                    var result = memoryStream.ToArray();
                    await blockBlob.Upload​From​Byte​Array​Async(result, 0, result.GetLength(0) - 1);
                }
            }

            return SetupFile(formFile, info);
        }

        public override async Task<byte[]> GetAsync(Administration.Entities.File file)
        {
            var components = file.Path.Split(Path.DirectorySeparatorChar).ToList();
            var containerName = components.First();
            var filename = components.Last();

            var container = GetContainer(containerName);
            if (!await container.ExistsAsync())
            {
                throw new Exception("Container not found");
            }

            var blockBlob = container.GetBlockBlobReference(filename);

            byte[] result = null;

            using (var memoryStream = new MemoryStream())
            {
                await blockBlob.DownloadToStreamAsync(memoryStream);
                result = memoryStream.ToArray();
            } 

            return result;
        }

        private CloudBlobContainer GetContainer(string name)
        {
            var blobClient = StorageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference(name);
        }

        private async Task<CloudBlobContainer> CreateContainer(string name)
        {
            var blobClient = StorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(name);
            var containerCreated = await container.CreateIfNotExistsAsync();
            if (!containerCreated)
            {
                throw new Exception("Could not create container");
            }

            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            return container;
        }

        private (Guid guid, string name, string ext) GetFileInfo(IFormFile formFile)
        {
            var ext = formFile.FileName.Split('.').Last();
            var guid = Guid.NewGuid();
            var name = $"{guid}.{ext}";

            return (guid, name, ext);
        }

        private Administration.Entities.File SetupFile(IFormFile formFile, (Guid guid, string name, string ext) fileInfo)
        {
            return new Administration.Entities.File()
            {
                Guid = fileInfo.guid,
                StorageType = StorageTypeEnum.Azure,
                User = User,
                Path = $"{ContainerName}{Path.DirectorySeparatorChar}{fileInfo.name}",
                ContentType = formFile.ContentType,
                Name = formFile.FileName,
                Extension = fileInfo.ext
            };
        }
    }
}
