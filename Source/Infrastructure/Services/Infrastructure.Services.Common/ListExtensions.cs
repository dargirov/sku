using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Services.Common
{
    public static class ListExtensions
    {
        public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> list, Func<T, string> valueFunc, Func<T, string> textFunc, string defaultValue, bool createEmptyElement)
        {
            var result = new List<SelectListItem>();
            if (!list.Any())
            {
                return result;
            }

            if (createEmptyElement)
            {
                result.Add(new SelectListItem()
                {
                    Value = "0",
                    Text = "",
                    Selected = false
                });
            }

            result.AddRange(list.Select(resultItem => new SelectListItem
            {
                Value = valueFunc(resultItem),
                Text = textFunc(resultItem),
                Selected = defaultValue == valueFunc(resultItem)
            }));

            return result;
        }
    }
}
