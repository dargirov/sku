using Administration.Bll;
using Administration.Entities;
using Infrastructure.Services.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services.ContentServer
{
    public class ContentServer : IContentServer
    {
        private IConfigurationRoot Configuration { get; }
        //private StorageType StorageType { get; set; }
        private ContentServerContext Context { get; }
        //private int OrganizationId => cacheService.Get<int>("organization_id");
        private int UserId => 1; // cacheService.Get<int>("user_id");
        //private readonly ICacheService cacheService;
        //private readonly IUsersService userService;
        private readonly IFileServices filesService;

        public ContentServer(IHostingEnvironment environment, IFileServices filesService)
        {
            //this.cacheService = cacheService;
            //this.userService = userService;
            this.filesService = filesService;

            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false);
            Configuration = builder.Build();

            var user = new User() { OrganizationId = 1 }; //userService.GetById(UserId);

            var storageType = GetAvailableStorageType();

            switch (storageType)
            {
                case StorageType.Azure:
                    Context = new ContentServerContext(new AzureStorageClient(user, Configuration["ContentServer:AzureStorage:Credentials:AccountName"], Configuration["ContentServer:AzureStorage:Credentials:Key1"]));
                    break;
                case StorageType.Local:
                    Context = new ContentServerContext(new LocalStorageClient(user, Configuration["ContentServer:LocalStorage:Folder"]));
                    break;
            }
        }

        private StorageType GetAvailableStorageType()
        {
            var isLocal = Utils.TryParseBool(Configuration["ContentServer:StorageType:Local"]);
            var isAzure = Utils.TryParseBool(Configuration["ContentServer:StorageType:Azure"]);

            if (!isLocal.HasValue || !isAzure.HasValue)
            {
                throw new Exception("Can not find storage type option");
            }

            if (isLocal.Value && isAzure.Value)
            {
                throw new Exception("Both Local and Azure storage providers are active at the same time");
            }

            if (isLocal.Value)
            {
                return StorageType.Local;
            }

            if (isAzure.Value)
            {
                return StorageType.Azure;
            }

            throw new Exception("Unknown content server storage provider");
        }

        public async Task<File> SaveAsync(IFormFile formFile)
        {
            var file = await Context.SaveAsync(formFile);
            await filesService.SaveAsync(file);
            return file;
        }

        public async Task<File> ResizeAndSaveAsync(IFormFile formFile)
        {
            var allowedImageFormats = Configuration.GetSection("ContentServer:AllowedContentTypes").GetChildren().Select(x => x.Value).ToList();

            if (!allowedImageFormats.Contains(formFile.ContentType))
            {
                throw new FormatException("Unsupported content type");
            }

            var width = Utils.TryParseInt(Configuration["ContentServer:ImageThumbWidth"]);
            var height = Utils.TryParseInt(Configuration["ContentServer:ImageThumbHeight"]);

            if (!width.HasValue || !height.HasValue)
            {
                throw new Exception("ImageThumbWidth or ImageThumbHeight is not set in config file");
            }

            var file = await Context.ResizeAndSaveAsync(formFile, width.Value, height.Value);
            await filesService.SaveAsync(file);
            return file;
        }

        public async Task<byte[]> GetAsync(File file)
        {
            return await Context.GetAsync(file);
        }
    }
}
