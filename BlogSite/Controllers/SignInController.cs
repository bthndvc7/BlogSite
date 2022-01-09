using System;
using System.Collections.Generic;
using BlogSite.BussinessLayer;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Controllers
{
    public class SignInController : Controller
    {
        ArticleManager repoArticle = new ArticleManager();
        AuthorManager repoAuthor = new AuthorManager();
        public ActionResult Login(string Email, string Password)
        {
            if (Email == null)
            {
                return View();
            }
            else
            {
                var repo = repoAuthor.List();
                var model = repo.FirstOrDefault(m => m.Email == Email && m.Password == Password);
                if (model != null)
                {
                    Session["Email"] = model.Email;
                    Session["AuthorName"] = model.NameSurname;
                    if (model.Role == "Admin")
                    {
                        Session["Admin"] = "Admin";
                    }
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.Error = "Kullanıcı adı veya şifre hatalı";
                    return View();
                }
            }

        }
        
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}