using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineTest.Infrastructure
{
    public class TimeElapsedFilter:ActionFilterAttribute
    {
        Stopwatch SP = new Stopwatch();

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            SP.Reset();
            SP.Start();
            filterContext.HttpContext.Session["Time"] = SP;
        }
    }

    public class CheckTime:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var Time = filterContext.HttpContext.Session["Time"] as Stopwatch;
            Time.Stop();
            var elapsedTime = Time.Elapsed;
            var elapsedSeconds = elapsedTime.Seconds;
            if (elapsedSeconds > 17)
            {
                RouteValueDictionary dictionary = new RouteValueDictionary();
                dictionary.Add("Controller", "Test");
                dictionary.Add("Action", "Index");
                dictionary.Add("id", "Skipped");

                filterContext.Result = new RedirectToRouteResult(dictionary);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}