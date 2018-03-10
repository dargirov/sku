using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using StructureMap;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public class WidgetProvider : IWidgetProvider
    {
        private readonly IContainer _container;

        public WidgetProvider(IContainer container, IViewComponentHelper viewComponentHelper)
        {
            _container = container;
        }

        public async Task<IHtmlContent> Get(string name, IViewComponentHelper viewComponentHelper, object args)
        {
            if (!name.EndsWith("Widget"))
            {
                name += "Widget";
            }

            foreach (var widget in _container.GetAllInstances<IWidget>())
            {
                if (widget.Name == name)
                {
                    return await viewComponentHelper.InvokeAsync(name, args);
                }
            }

            return null;
        }

        public async Task<IHtmlContent> Get(string name, IViewComponentHelper viewComponentHelper)
        {
            return await Get(name, viewComponentHelper, new { });
        }
    }
}
