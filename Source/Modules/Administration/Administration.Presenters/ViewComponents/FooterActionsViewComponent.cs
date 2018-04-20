using Administration.Presenters.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace Administration.Presenters.ViewComponents
{
    public class FooterActionsViewComponent : ViewComponent
    {
        private IStringLocalizer<FooterActionsViewComponent> _localizer;

        public FooterActionsViewComponent(IStringLocalizer<FooterActionsViewComponent> localizer)
        {
            _localizer = localizer;
        }

        public async Task<IViewComponentResult> InvokeAsync(string controller, string action = "index", string deleteAction = "delete", string nextAction = "next", int id = 0, int routeParam = 0, bool showReset = true, bool showEdit = true, bool showDelete = true, bool showNextStep = false)
        {
            var viewModel = new FooterActionsViewModel()
            {
                Id = id,
                RouteParam = routeParam,
                Controller = controller.ToLower(),
                Action = action.ToLower(),
                DeleteAction = deleteAction.ToLower(),
                NextAction = nextAction.ToLower(),
                LocalizedReset = _localizer["Reset"],
                LocalizedBack = _localizer["Back"],
                LocalizedDelete = _localizer["Delete"],
                LocalizedNextStep = _localizer["Next"],
                ShowReset = showReset,
                ShowEdit = showEdit,
                ShowDelete = showDelete && id > 0,
                ShowNextStep = showNextStep,
                LocalizedEdit = id > 0 ? _localizer["Edit"] : _localizer["Create"]
            };

            return View(viewModel);
        }
    }
}
