using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "AuthorDetail",
                url: "yazarlar/{authorName}",
                defaults: new { controller = "Author", action = "Index", String = "" }
            );

            routes.MapRoute(
                name: "Admin",
                url: "Admin",
                defaults: new { controller = "Admin", action = "Index", String = "" }
            );
            routes.MapRoute(
                name: "Login",
                url: "Login",
                defaults: new { controller = "SignIn", action = "Login", String = "" }
            );
            routes.MapRoute(
                name: "Logout",
                url: "LogOut",
                defaults: new { controller = "SignIn", action = "LogOut", String = "" }
            );
            routes.MapRoute(
                name: "CreateAuthor",
                url: "CreateAuthor",
                defaults: new { controller = "Admin", action = "CreateAuthor", String = "" }
            );
            routes.MapRoute(
                name: "CreateArticle",
                url: "CreateArticle",
                defaults: new { controller = "Admin", action = "CreateArticle", String = "" }
            );
            routes.MapRoute(
                name: "EditArticle",
                url: "EditArticle/{id}",
                defaults: new { controller = "Admin", action = "EditArticle", String = "" }
            );
            routes.MapRoute(
                name: "EditAuthor",
                url: "EditAuthor/{id}",
                defaults: new { controller = "Admin", action = "EditAuthor", String = "" }
            );
            routes.MapRoute(
                name: "ListAuthor",
                url: "ListAuthor",
                defaults: new { controller = "Admin", action = "ListAuthor", String = "" }
            );
            routes.MapRoute(
                name: "ArticleDetail",
                url: "{CategoryName}/{linkUrl}",
                defaults: new { controller = "Home", action = "Detail", String = "" }
            );
            routes.MapRoute(
                name: "Category",
                url: "{CategoryName}",
                defaults: new { controller = "Category", action = "Index", String = "" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
