using System.Web.Mvc;
using System.Web.Routing;

namespace IdentitySample
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
       name: "Default",
       url: "{controller}/{action}/{id}",
       defaults: new { controller = "Home", action = "index", id = UrlParameter.Optional }
   );



            routes.MapRoute(
          "Customer",
          "Customer/{controller}/{action}/{id}",
          new { controller = "CustomerMenu", action = "Index", id = UrlParameter.Optional });
            
      

            routes.MapRoute(
         "Managements",
         "Managements/{controller}/{action}/{id}",
         new { controller = "Managements", action = "Index", id = UrlParameter.Optional });

   


        }


    }
}