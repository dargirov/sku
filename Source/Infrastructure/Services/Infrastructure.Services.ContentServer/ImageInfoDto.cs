using Infrastructure.Services.Common.Mappings;
using Product.Entities;

namespace Infrastructure.Services.ContentServer
{
    public class ImageInfoDto : IMapTo<ProductPicture>
    {
        public string ContentType { get; internal set; }
        public string Extension { get; internal set; }
        public string Name { get; internal set; }
        public string PathFullSize { get; internal set; }
        public string PathThumb { get; internal set; }
    }
}