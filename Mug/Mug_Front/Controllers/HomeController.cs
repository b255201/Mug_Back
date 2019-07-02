using Mug.Service;
using Mug.Service.Interface;
using Mug_Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mug_Front.Controllers
{
    public class HomeController : Controller
    {
        private IBloggerService BloggerService = new BloggerService();
        private ILanguageService LanguageService = new LanguageService();
        private IArticleService ArticleService = new ArticleService();
        public ActionResult Index()
        {
            var  blog = BloggerService.GetAll();
            var articles = ArticleService.GetAll();
            var result = from b in blog
                          join a in articles
                           on b.Blog_id equals a.Post_Id
                          where b.Categore == "首頁大圖" && b.Enable==true
                          orderby b.Blog_id
                          select new ArticleViewModel
                          {
                              Blog_id = b.Blog_id,
                              Title = a.Title,
                              Image = b.Image,
                              Enable = b.Enable,
                              Categore=b.Categore,
                              Sub_Title=a.Sub_Title,
                              Contents=a.Contents,   
                              Id=a.Id
                          };
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
            {
                LanguageCommon LangUser = new LanguageCommon();
               string LangKey=LangUser.GetUserLang(cookie.Value.ToString());
                result = result.Where(x => x.Id == int.Parse(LangKey)).ToList();
            }
            else
            {
                result = result.Where(x => x.Id == 1).ToList();
            }
            return View(result);
        }

        public ActionResult GetImageFile(string fileName)
        {
            //return File("../../Image/Home/" + fileName, "image/png");
            return File("C:/Users/Administrator/Desktop/MUG_Back/Mug/Mug/Image/Home/" + fileName, "image/png");
        }

    }
}