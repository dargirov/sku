using Administration.Presenters.Dtos;
using Infrastructure.Data.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Administration.Presenters.ViewComponents
{
    public class PagerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string action, PageData pageData, string gridId)
        {
            var viewModel = new PagerViewModel()
            {
                Action = action,
                PageData = pageData,
                GridId = gridId
            };

            return View(viewModel);
        }
    }
}
