using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Utils
{
    public class BaseController : Controller
    {
        public string Email { get; set; }
        public string AuthorName { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Email"] == null)
            {
                filterContext.Result = new RedirectResult("/Login");
            }
            else
            {
                Email = Session["Email"].ToString();
                AuthorName = Session["AuthorName"].ToString();
            }
            base.OnActionExecuting(filterContext);
        }
    }
}