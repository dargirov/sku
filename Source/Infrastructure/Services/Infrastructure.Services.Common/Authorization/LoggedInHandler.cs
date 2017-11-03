using Infrastructure.Services.Common;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Infrastructure.Services.Common.Authorization
{

    public class LoggedInHandler : AuthorizationHandler<LoggedInRequirement>
    {
        private readonly ICacheServices _cacheServices;

        public LoggedInHandler(ICacheServices cacheServices)
        {
            _cacheServices = cacheServices;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LoggedInRequirement requirement)
        {
            var loggedin = _cacheServices.Get<bool>("logged_in");
            if (loggedin)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
