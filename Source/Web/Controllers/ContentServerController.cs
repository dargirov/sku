using Administration.Bll;
using Infrastructure.Services.Common;
using Infrastructure.Services.ContentServer;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class ContentServerController : Controller
    {
        private readonly IContentServer _contentServer;
        private readonly IFileServices _fileServices;

        public ContentServerController(IContentServer contentServer, IFileServices fileServices)
        {
            _contentServer = contentServer;
            _fileServices = fileServices;
        }

        public async Task<IActionResult> Index(string id)
        {
            var guid = Utils.TryParseGuid(id);
            if (!guid.HasValue)
            {
                return BadRequest();
            }

            var file = await _fileServices.GetAsync(guid.Value);
            if (file == null)
            {
                return NotFound();
            }

            var fileBytes = await _contentServer.GetAsync(file);

            return File(fileBytes, file.ContentType);
        }
    }
}
