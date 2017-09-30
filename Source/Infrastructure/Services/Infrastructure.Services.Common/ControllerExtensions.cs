using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Services.Common
{
    public static class ControllerExtensions
    {
        public static List<string> GetErrors(this ModelStateDictionary modelState)
        {
            var result = new List<string>();
            result.AddRange(modelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            return result;
        }
    }
}
