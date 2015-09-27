using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

public class UseStopwatchAttribute : ActionFilterAttribute
{
   public override void OnActionExecuting(ActionExecutingContext filterContext)
   {
      Stopwatch stopWatch = new Stopwatch();
      stopWatch.Start();

      filterContext.Controller.ViewData["stopWatch"] = stopWatch;
      filterContext.Controller.ViewBag.stopWatch = stopWatch;
   }

   // This is our last chance to log timing info to a view. You must set 
   // SkipTimeInLayout > 0  to skip the output.

   public override void OnResultExecuting(ResultExecutingContext filterContext)
   {
      if (AppSettings.SkipTimeInLayout() > 0)
      {
         return;
      }

      Stopwatch stopWatch = (Stopwatch)filterContext.Controller.ViewBag.stopWatch;

      double et = stopWatch.Elapsed.Seconds +
         (stopWatch.Elapsed.Milliseconds / 1000.0);

      filterContext.Controller.ViewBag.elapsedTime = "Elapsed time: " +
           et.ToString();
   }

   public override void OnResultExecuted(ResultExecutedContext filterContext)
   {
      Stopwatch stopWatch = (Stopwatch)filterContext.Controller.ViewBag.stopWatch;
      stopWatch.Stop();

      double et = stopWatch.Elapsed.Seconds +
         (stopWatch.Elapsed.Milliseconds / 1000.0);

      string msg = "ET: " + et.ToString();

      var routeDataKeys = filterContext.RouteData.Values.Keys.ToArray();
      string strKey = string.Empty;

      foreach (var key in routeDataKeys)
      {
         msg += " " + key + ": " + filterContext.RouteData.Values[key];
      }

      var qs = filterContext.RequestContext.HttpContext.Request.QueryString.ToString();
      if (!String.IsNullOrEmpty(qs))
      {
         msg += " QS: " + qs;
      }

      TraceMessage(et, msg);
   }

   void TraceMessage(double et, string msg)
   {
      if (et > AppSettings.TraceErrorTime())
      {
         Trace.TraceError(msg);
      }
      else if (et > AppSettings.TraceWarningTime())
      {
         Trace.TraceWarning(msg);
      }
      else if (et > AppSettings.TraceInformationTime())
      {
         Trace.TraceInformation(msg);
      }
      else
      {
         Trace.WriteLine(msg);
      }
   }

}

public static class AppSettings
{
   static double dError = 0;
   static double dWarn = 0;
   static double dInfo = 0;
   static int skipLayoutTime = -1;

   static double AppStringToDouble(string key)
   {
      return Convert.ToDouble(ConfigurationManager.AppSettings[key]);
   }

   static int AppStringToInt(string key)
   {
      return Convert.ToInt32(ConfigurationManager.AppSettings[key]);
   }

   public static double TraceErrorTime()
   {
      if (dError == 0)
         dError = AppStringToDouble("TraceErrorTime");

      return dError;
   }

   public static double TraceWarningTime()
   {
      if (dWarn == 0)
         dWarn = AppStringToDouble("TraceWarningTime");

      return dWarn;
   }

   public static double TraceInformationTime()
   {
      if (dInfo == 0)
         dInfo = AppStringToDouble("TraceInformationTime");

      return dInfo;
   }

   public static int SkipTimeInLayout()
   {
      if (skipLayoutTime < 0)
         skipLayoutTime = AppStringToInt("SkipTimeInLayout");

      return skipLayoutTime;
   }
}