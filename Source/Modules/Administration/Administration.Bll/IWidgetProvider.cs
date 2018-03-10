using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public interface IWidgetProvider
    {
        Task<IHtmlContent> Get(string name, IViewComponentHelper viewComponentHelper);
        Task<IHtmlContent> Get(string name, IViewComponentHelper viewComponentHelper, object args);
    }
}
