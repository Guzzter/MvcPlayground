using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using UkAdcHtmlAttributeProvider.Infrastructure;

namespace MvcPlayground
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Using Nuget package: UkAdcHtmlAttributeProvider
            // http://blogs.msdn.com/b/ukadc/archive/2012/05/24/asp-net-mvc-adding-html-attributes-in-templated-helpers-editorfor.aspx
            // When [Required] is uses, textbox gets aria-required=true
            HtmlAttributeProvider.Register(metadata => metadata.IsRequired, "aria-required", true);
            // When [Required] is uses, textbox gets aria-required=true
            HtmlAttributeProvider.Register(metadata => metadata.DataTypeName == "Date", "class", "date");

        }
    }
}