See http://blogs.msdn.com/b/webdev/archive/2014/07/29/profile-and-time-your-asp-net-mvc-app-all-the-way-to-azure.aspx
for more info.

To register this filter you need to add the following line to the App_Start\FilterConfig.cs file:

    filters.Add(new UseStopwatchAttribute());

For Example: 
 
     public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new UseStopwatchAttribute());  // Add this line
        }
    }
    
Alternatively, to get the timings on specific controllers, add the [UseStopwatch] attribute
to the controller. For example:

 [UseStopwatch]
 public class HomeController : Controller
    

 </connectionStrings>
   <appSettings>
      <add key="webpages:Version" value="3.0.0.0" />
      <!-- Keys deleted for clarity.  -->
     
      <add key="TraceErrorTime" value="2.5" />
      <add key="TraceWarningTime" value=".5" />
      <add key="TraceInformationTime" value=".25" />
      <add key="SkipTimeInLayout" value="1" />
   </appSettings>
   <system.web>
   
   To see the time in each page, add the following code to the Views\Shared\_Layout.cshtml file:
   
  <footer>
     <p> @Html.Encode(ViewBag.elapsedTime) </p>
  </footer>