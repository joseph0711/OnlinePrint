using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlinePrint
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}",
                new { botDetect = @"(.*)BotDetectCaptcha\.ashx" });
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "login", action = "login", id = UrlParameter.Optional }
            );
        }
    }
}
