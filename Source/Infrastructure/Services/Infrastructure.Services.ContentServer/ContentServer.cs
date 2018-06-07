using Administration.Bll;
using Administration.Entities;
using Infrastructure.Data.Common;
using Infrastructure.Services.Common;
using Infrastructure.Utils;
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
        //private int OrganizationId => _cacheServices.Get<int>("organization_id");
        //private int UserId => _cacheServices.Get<int>("user_id");
        private readonly ICacheServices _cacheServices;
        private readonly IFileServices _filesServices;

        public ContentServer(IHostingEnvironment environment, IFileServices filesServices, ICacheServices cacheServices)
        {
            _cacheServices = cacheServices;
            _filesServices = filesServices;

            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false);
            Configuration = builder.Build();

            var userId = _cacheServices.Get<int>("user_id");
            var organizationId = _cacheServices.Get<int>("organization_id");

            if (userId == default(int) || organizationId == default(int))
            {
                throw new Exception("Can not get user id or organization id");
            }

            var storageType = GetAvailableStorageType();

            switch (storageType)
            {
                case StorageTypeEnum.Azure:
                    Context = new ContentServerContext(new AzureStorageClient(userId, organizationId, Configuration["ContentServer:AzureStorage:Credentials:AccountName"], Configuration["ContentServer:AzureStorage:Credentials:Key1"]));
                    break;
                case StorageTypeEnum.Local:
                    Context = new ContentServerContext(new LocalStorageClient(userId, organizationId, Configuration["ContentServer:LocalStorage:Folder"]));
                    break;
            }
        }

        private StorageTypeEnum GetAvailableStorageType()
        {
            var isLocal = Parse.TryParseBool(Configuration["ContentServer:StorageType:Local"]);
            var isAzure = Parse.TryParseBool(Configuration["ContentServer:StorageType:Azure"]);

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
                return StorageTypeEnum.Local;
            }

            if (isAzure.Value)
            {
                return StorageTypeEnum.Azure;
            }

            throw new Exception("Unknown content server storage provider");
        }

        public async Task<File> SaveAsync(IFormFile formFile)
        {
            var messages = new Messages();
            var file = await Context.SaveAsync(formFile);
            await _filesServices.SaveAsync(file, messages);
            return file;
        }

        public async Task<File> ResizeAndSaveAsync(IFormFile formFile)
        {
            var allowedImageFormats = Configuration.GetSection("ContentServer:AllowedContentTypes").GetChildren().Select(x => x.Value).ToList();

            if (!allowedImageFormats.Contains(formFile.ContentType))
            {
                throw new FormatException("Unsupported content type");
            }

            var width = Parse.TryParseInt(Configuration["ContentServer:ImageThumbWidth"]);
            var height = Parse.TryParseInt(Configuration["ContentServer:ImageThumbHeight"]);

            if (!width.HasValue || !height.HasValue)
            {
                throw new Exception("ImageThumbWidth or ImageThumbHeight is not set in config file");
            }

            var messages = new Messages();
            var file = await Context.ResizeAndSaveAsync(formFile, width.Value, height.Value);
            await _filesServices.SaveAsync(file, messages);
            return file;
        }

        public async Task<byte[]> GetAsync(File file)
        {
            return await Context.GetAsync(file);
        }
    }
}
