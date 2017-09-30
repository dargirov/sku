using Administration.Presenters.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace Administration.Presenters.ViewComponents
{
    public class FooterActionsViewComponent : ViewComponent
    {
        private IStringLocalizer<FooterActionsViewComponent> localizer;

        public FooterActionsViewComponent(IStringLocalizer<FooterActionsViewComponent> localizer)
        {
            this.localizer = localizer;
        }

        public async Task<IViewComponentResult> InvokeAsync(string controller, string action = "index", string deleteAction = "delete", int id = 0, int routeParam = 0, bool showReset = true, bool showDelete = true)
        {
            var viewModel = new FooterActionsViewModel()
            {
                Id = id,
                RouteParam = routeParam,
                Controller = controller.ToLower(),
                Action = action.ToLower(),
                DeleteAction = deleteAction.ToLower(),
                LocalizedReset = localizer["Reset"],
                LocalizedBack = localizer["Back"],
                LocalizedDelete = localizer["Delete"],
                ShowReset = showReset,
                ShowDelete = showDelete && id > 0,
                LocalizedEdit = id > 0 ? localizer["Edit"] : localizer["Create"]
            };

            return View(viewModel);
        }
    }
}
