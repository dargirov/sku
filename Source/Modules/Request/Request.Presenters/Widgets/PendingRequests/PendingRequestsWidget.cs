using Administration.Bll;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Mvc;
using Request.Bll;
using System.Linq;
using System.Threading.Tasks;

namespace Request.Presenters.Widgets.PendingRequests
{
    public class PendingRequestsWidget : ViewComponent, IWidget
    {
        private readonly IRequestServices _requestServices;

        public PendingRequestsWidget(IRequestServices requestServices)
        {
            _requestServices = requestServices;
        }

        public string Name => nameof(PendingRequestsWidget);

        public async Task<IViewComponentResult> InvokeAsync(string page, int limit)
        {
            int currentPage = Parse.TryParseInt(page) ?? 1;

            var (requests, pageData) = await _requestServices.GetListAsync(currentPage, limit, Entities.RequestStatusEnum.New, Entities.RequestStatusEnum.PartiallyCompleted);

            var viewModel = new ViewModel()
            {
                Requests = requests.Select(x => new RequestDto()
                {
                    Id = x.Id,
                    CreatedOn = x.CreatedOn,
                    Status = x.Status.ToString(),
                    ProductCount = x.StockRequests.Count,
                    HasHightPriorityProduct = x.StockRequests.Any(s => s.Priority == Product.Entities.ProductPriorityEnum.High)
                }),
                PageData = pageData
            };

            return View(viewModel);
        }
    }
}
