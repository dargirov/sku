using AutoMapper;
using Infrastructure.Data.Common;
using Infrastructure.Services.Common.Mappings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Administration.Presenters
{
    public class BaseController : Controller
    {
        public IMapper Mapper => AutoMapperConfig.Mapper;
        public Messages Messages { get; } = new Messages();

        protected bool IsAjax()
        {
            if (!Request.Headers.ContainsKey("X-Requested-With") || Request.Headers["X-Requested-With"] != "XMLHttpRequest")
            {
                return false;
            }

            return true;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            SetupTempMessages();
        }

        private void SetupTempMessages()
        {
            if (Messages.HasErrors)
            {
                TempData["HasErrors"] = true;
                TempData["Errors"] = Messages.GetErrors();
            }

            if (Messages.HasWarnings)
            {
                TempData["HasWarnings"] = true;
                TempData["Warnings"] = Messages.GetWarnings();
            }

            if (Messages.HasSuccesses)
            {
                TempData["HasSuccesses"] = true;
                TempData["Successes"] = Messages.GetSuccesses();
            }
        }
    }
}
