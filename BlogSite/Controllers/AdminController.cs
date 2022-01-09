using System;
using BlogSite.BussinessLayer;
using BlogSite.Models;
using BlogSite.DataEntities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogSite.Utils;
using Oracle.ManagedDataAccess.Client;

namespace BlogSite.Controllers
{
    public class AdminController : BaseController
    {
        ArticleManager repoArticle = new ArticleManager();
        AuthorManager repoAuthor = new AuthorManager();
        // GET: Admin
        public ActionResult Index()
        {
            var model = repoArticle.List();
            return View(model);
        }

        public ActionResult ListAuthor()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "SignIn");
            }

            var model = repoAuthor.List();
            return View(model);
        }

        public ActionResult CreateAuthor(Author authorModel, string AdminRadio)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "SignIn");
            }

            if (authorModel.NameSurname != null)
            {
                if (AdminRadio == "Admin")
                {
                    authorModel.Role = "Admin";
                }
                else
                {
                    authorModel.Role = "Author";
                }
                repoAuthor.Insert(authorModel);
                return RedirectToAction("ListAuthor");
            }
            else
            {
                return View();
            }
            
        }
        public ActionResult CreateArticle(Article articleModel)
        {
            if (articleModel.Content != null)
            {
                articleModel.Author = AuthorName;
                articleModel.ArticleDate = DateTime.Now.ToString();
                articleModel.IsActive = 1 ;
                articleModel.ReadingCount = 50;
                articleModel.ArticleUrl = articleModel.Title;
                repoArticle.Insert(articleModel);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        public ActionResult DeleteAuthor(int Id)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "SignIn");
            }

            var model = repoAuthor.GeyById(Id);
            repoAuthor.Delete(model);
            return RedirectToAction("ListAuthor");
        }
        public ActionResult DeleteArticle(int Id)
        {

            var model = repoArticle.GeyById(Id);
            repoArticle.Delete(model);
            return RedirectToAction("Index");
        }


        public ActionResult EditAuthor(Author authorDb, string AdminRadio)
        {

            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "SignIn");
            }

            if (authorDb.NameSurname != null)
            {
                if (AdminRadio == "Admin")
                {
                    authorDb.Role = "Admin";
                }
                else
                {
                    authorDb.Role = "Author";
                }
                ArticleByCategory article = new ArticleByCategory();
                article.AuthorName = authorDb.NameSurname;

                string connString = System.Configuration.ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString;
                OracleConnection conn = new OracleConnection(connString);


                string editString = "UPDATE \"Author\" SET \"NameSurname\" = :namesurname, \"AuthorAbout\" = :authorabout, \"Image\" = :image," +
                    "\"Email\" = :email, \"Password\" = :password, \"FacebookAdress\" = :facebook, \"TwitterAdress\" = :twitter, " +
                    "\"InstagramAdress\" = :instagram, \"Role\" = :role WHERE \"Id\" = :id";

                conn.Open();

                OracleCommand command = conn.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = editString;


                command.Parameters.Add("namesurname", authorDb.NameSurname);
                command.Parameters.Add("authorabout", authorDb.AuthorAbout);
                command.Parameters.Add("image", authorDb.Image);
                command.Parameters.Add("email", authorDb.Email);
                command.Parameters.Add("password", authorDb.Password);
                command.Parameters.Add("facebook", authorDb.FacebookAdress);
                command.Parameters.Add("twitter", authorDb.TwitterAdress);
                command.Parameters.Add("instagram", authorDb.InstagramAdress);
                command.Parameters.Add("role", authorDb.Role);
                command.Parameters.Add("id", authorDb.Id);

                


                command.ExecuteNonQuery();
                conn.Close();

                

                return RedirectToAction("ListAuthor");

            }
            else
            {       
                AuthorModel authorModel = (from author in repoAuthor.List()                                                 
                                           where author.Id == authorDb.Id
                                           select new AuthorModel
                                           {
                                               //Author
                                               AuthorId = author.Id,
                                               AuthorAbout = author.AuthorAbout,
                                               AuthorFacebook = author.FacebookAdress,
                                               AuthorImage = author.Image,
                                               AuthorInstagram = author.InstagramAdress,
                                               AuthorName = author.NameSurname,
                                               AuthorTwitter = author.TwitterAdress,
                                               AuthorEmail = author.Email,
                                               AuthorPassword = author.Password,
                                           }).FirstOrDefault();

                return View(authorModel);
            }
        }
        public ActionResult EditArticle(Article articleDb)
        {

            if (articleDb.Content != null)
            {
                articleDb.Author = Session["AuthorName"].ToString();
                articleDb.ArticleDate = DateTime.Now.ToString();
                articleDb.IsActive = 1;
                articleDb.ReadingCount = 50;
                articleDb.ArticleUrl = articleDb.Title;

                string connString = System.Configuration.ConfigurationManager.ConnectionStrings["DataContext"].ConnectionString;
                OracleConnection conn = new OracleConnection(connString);


                string editString = "UPDATE \"Article\" SET \"Author\" = :author, \"CategorName\" = :category, \"ArticleDate\" = :articleDate," +
                    "\"Title\" = :title, \"ImageUrl\" = :imgUrl, \"Content\" = :content, \"ArticleUrl\" = :articleUrl, \"Tags\" = :tags WHERE \"Id\" = :id";

                conn.Open();

                OracleCommand command = conn.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = editString;


                command.Parameters.Add("author", articleDb.Author);
                command.Parameters.Add("category", articleDb.CategorName);
                command.Parameters.Add("articleDate", articleDb.ArticleDate);
                command.Parameters.Add("title", articleDb.Title);
                command.Parameters.Add("imgUrl", articleDb.ImageUrl);
                command.Parameters.Add("content", articleDb.Content);
                command.Parameters.Add("articleUrl", articleDb.ArticleUrl);
                command.Parameters.Add("tags", articleDb.Tags);
                command.Parameters.Add("id", articleDb.Id);


                command.ExecuteNonQuery();
                conn.Close();
                return RedirectToAction("Index");

            }
            else
            {
                ArticleViewModel articleModel = (from article in repoArticle.List()
                                                 join author in repoAuthor.List() on article.Author equals author.NameSurname
                                                 where article.Id == articleDb.Id
                                                 select new ArticleViewModel
                                                 {
                                                     //Article
                                                     ArticleCategory = article.CategorName,
                                                     Content = article.Content,
                                                     Title = article.Title,
                                                     ArticleTags = article.Tags.Split(','),
                                                     AuthorImageUrl = article.ImageUrl,
                                                     ArticleReading = article.Id
                                                 }).FirstOrDefault();

                return View(articleModel);
            }
        }

    }
}


