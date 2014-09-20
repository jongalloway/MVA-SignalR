using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealTimeTracingWithAngular.Filters
{
    public class TraceFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var routeData = filterContext.RouteData;

            var controller = routeData.Values["controller"];
            var action = routeData.Values["action"];

            Trace.WriteLine(
                string.Format(@"Action Method {0}/{1} was hit at {2}",
                    controller,
                    action,
                    DateTime.Now.ToLongTimeString()
                ));

            base.OnActionExecuting(filterContext);
        }
    }
}