using ImageSharp;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Services.ContentServer
{
    public class LocalStorageClient : StorageClient
    {
        private string ContentPath { get; set; }
        private string Subdirname { get; set; }
        private int UserId { get; set; }

        public LocalStorageClient(int userId, int organizationId, string path)
        {
            ContentPath = path;
            Subdirname = $"organization{organizationId}";
            UserId = userId;
        }

        public override async Task<Administration.Entities.File> SaveAsync(IFormFile formFile)
        {
            var dir = CreateDir();
            var info = GetFileInfo(dir, formFile.FileName);

            using (var fileStream = new FileStream(info.path, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            return SetupFile(formFile, info);
        }

        public override async Task<Administration.Entities.File> ResizeAndSaveAsync(IFormFile formFile, int width, int height)
        {
            var dir = CreateDir();
            var info = GetFileInfo(dir, formFile.FileName);

            using (var readStream = formFile.OpenReadStream())
            {
                var image = new Image(readStream);

                using (var writeStream = new FileStream(info.path, FileMode.Create))
                {
                    image.Resize(width, height).Save(writeStream);
                }
            }

            return SetupFile(formFile, info);
        }

        public override async Task<byte[]> GetAsync(Administration.Entities.File file)
        {
            return File.ReadAllBytes(Path.Combine(Directory.GetParent(ContentPath).ToString(), file.Path));
        }

        private Administration.Entities.File SetupFile(IFormFile formFile, (Guid guid, string path, string ext) info)
        {
            return new Administration.Entities.File()
            {
                Guid = info.guid,
                StorageType = Administration.Entities.StorageTypeEnum.Local,
                UserId = UserId,
                Path = info.path,
                ContentType = formFile.ContentType,
                Name = formFile.FileName,
                Extension = info.ext
            };
        }

        private (Guid guid, string path, string ext) GetFileInfo(string dir, string filename)
        {
            var ext = Path.GetExtension(Path.Combine(dir, filename)).Replace(".", string.Empty);
            var guid = Guid.NewGuid();
            var path = Path.Combine(dir, $"{guid}.{ext}");

            return (guid, path, ext);
        }

        private string CreateDir()
        {
            var path = Path.Combine(new string[] { ContentPath, Subdirname, DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString() });

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }
}
