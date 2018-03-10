using Administration.Bll;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Administration.Presenters.Widgets.Todos
{
    public class TodosWidget : ViewComponent, IWidget
    {
        public string Name => nameof(TodosWidget);

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
