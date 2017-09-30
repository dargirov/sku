//using Microsoft.AspNetCore.Mvc.Razor;
//using System.Collections.Generic;
//using System.Linq;

//namespace ModulesCommon
//{
//    public class ViewLocationExpander : IViewLocationExpander
//    {
//        private readonly string[] _locations;

//        public ViewLocationExpander(List<string> locations)
//        {
//            _locations = locations.ToArray();
//        }

//        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
//        {
//            return _locations.Union(viewLocations);
//        }

//        public void PopulateValues(ViewLocationExpanderContext context)
//        {
//            context.Values["customviewlocation"] = nameof(ViewLocationExpander);
//        }
//    }
//}
